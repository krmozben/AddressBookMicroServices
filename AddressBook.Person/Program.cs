using AddressBook.Person.Data.Data;
using AddressBook.Person.Data.Data.Implementation;
using AddressBook.Person.Extensions;
using AddressBook.Person.Service.PersonFeed;
using AddressBook.Person.Service.Services;
using AddressBook.Person.Service.Services.Implementation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Newtonsoft.Json;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
               .AddMongoDb("mongodb://localhost:27017",
                    name: "Mongo Db Check");


var mongoClientSettings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDB"));
mongoClientSettings.ReadConcern = ReadConcern.Majority;
mongoClientSettings.ReadPreference = ReadPreference.SecondaryPreferred;
mongoClientSettings.WriteConcern = WriteConcern.WMajority;
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<PersonFeed>();
builder.Services.AddScoped<IMongoDbDataContext, MongoDbDataContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MigrateDatabase(app.Services.GetRequiredService<ILogger<Program>>(), builder.Configuration);

app.Run();
