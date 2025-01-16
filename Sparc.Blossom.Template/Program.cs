using System.Configuration;
using Microsoft.Extensions.Configuration;
using Sparc.Blossom.Template;
using Sparc.Database.SqlServer;

var builder = BlossomApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddScoped<AuthenticationService>();

// Configure SQL Server database connection using connection string from appsettings.json
// Optionally: Add any other services your app needs here, such as authentication or repositories

var app = builder.Build();

// If you need to seed data, create a scope and add some random data


await app.RunAsync<Html>();
