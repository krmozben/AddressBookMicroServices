using AddressBook.Contacts.Application.AddressBookContextSeed;
using AddressBook.Contacts.Application.Clients.Redis;
using AddressBook.Contacts.Application.Configuration;
using AddressBook.Contacts.Application.Consumers;
using AddressBook.Contacts.Application.Repositories;
using AddressBook.Contacts.Application.Services;
using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Extensions;
using AddressBook.Contacts.Infrastructure;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
// Add services to the container.

builder.Services.AddDbContext<AddressBookDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        configure.MigrationsAssembly("AddressBook.Contacts.Infrastructure");
    });
});

builder.Services.Configure<RedisConfiguration>(builder.Configuration.GetSection("RedisConfiguration"));
builder.Services.AddSingleton<RedisClient>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisConfiguration>>().Value;

    var redis = new RedisClient(redisSettings.Host, redisSettings.Port);

    redis.Connect();

    return redis;
});


builder.Services.AddMediatR(Assembly.Load("AddressBook.Contacts.Application"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateContactConsumer>();
    x.AddConsumer<RemoveContactConsumer>();
    x.AddConsumer<UpdateContactConsumer>();
    x.AddConsumer<DeleteContactConsumer>();
    x.AddConsumer<AddContactInformationConsumer>();
    x.AddConsumer<RemoveContactInformationConsumer>();
    x.AddConsumer<UpdateAllContactsOnRedisConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("create-contact", e =>
        {
            e.ConfigureConsumer<CreateContactConsumer>(context);
        });

        cfg.ReceiveEndpoint("remove-contact", e =>
        {
            e.ConfigureConsumer<RemoveContactConsumer>(context);
        });


        cfg.ReceiveEndpoint("update-contact", e =>
        {
            e.ConfigureConsumer<UpdateContactConsumer>(context);
        });

        cfg.ReceiveEndpoint("delete-contact", e =>
        {
            e.ConfigureConsumer<DeleteContactConsumer>(context);
        });


        cfg.ReceiveEndpoint("add-contact-information", e =>
        {
            e.ConfigureConsumer<AddContactInformationConsumer>(context);
        }); 
        
        cfg.ReceiveEndpoint("remove-contact-information", e =>
        {
            e.ConfigureConsumer<RemoveContactInformationConsumer>(context);
        }); 
        
        cfg.ReceiveEndpoint("update-all-contact-on-redis", e =>
        {
            e.ConfigureConsumer<UpdateAllContactsOnRedisConsumer>(context);
        });
    });
});

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<AddressBookContextSeed>();
builder.Services.AddScoped(typeof(IRepository<Contact>), typeof(ContactsRepository));


builder.Services.AddHealthChecks()
                .AddSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    name: "AddressBook Management Sql Server Database")
                .AddElasticsearch(
                    builder.Configuration.GetValue<string>("ElasticSearchUrl"),
                    "AddressBook ElasticSearch Log Pool")
                .AddRedis("localhost",
                    name: "AddressBook Redis")
                 .AddRabbitMQ("amqp://guest:guest@localhost:5672",
                    name: "AddressBook RabbitMQ");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (c, r) =>
    {
        c.Response.ContentType = "application/json";

        var result = JsonConvert.SerializeObject(new
        {
            status = r.Status.ToString(),
            totalDuration = r.TotalDuration.ToString(),
            components = r.Entries.Select(e => new { name = e.Key, status = e.Value.Status.ToString(), duration = e.Value.Duration }),
        });
        await c.Response.WriteAsync(result);
    }
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MigrateDatabase(app.Services.GetRequiredService<ILogger<Program>>(), builder.Configuration);

app.Run();
