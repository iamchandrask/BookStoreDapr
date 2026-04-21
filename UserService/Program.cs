using System;
using System.Data.SqlClient;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Application;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configuration: appsettings.json, environment variables, etc.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add services to container
builder.Services.AddControllers().AddDapr();

// Dapr client
builder.Services.AddDaprClient();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("UserDatabase")
                       ?? throw new InvalidOperationException("Connection string 'UserDatabase' not found.");

// Register repository and application service
builder.Services.AddSingleton<IUserRepository>(sp => new UserRepository(connectionString));
builder.Services.AddScoped<IUserServiceApp, UserServiceApp>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCloudEvents();
app.UseRouting();

app.MapControllers();
app.MapSubscribeHandler();

app.Run();