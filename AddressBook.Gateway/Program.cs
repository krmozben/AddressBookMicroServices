using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

Console.WriteLine("Setting up Reverse Proxy");

builder.Services.AddOcelot();
var app = builder.Build();

app.UseOcelot();

app.Run();