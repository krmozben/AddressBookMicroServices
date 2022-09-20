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
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AddressBookDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        configure.MigrationsAssembly("AddressBook.Contacts.Infrastructure");
    });
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
    });
});

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped(typeof(IRepository<Contact>), typeof(ContactsRepository));


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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
