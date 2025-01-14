using System.Configuration;
using Microsoft.Extensions.Configuration;
using Sparc.Blossom.Template;
using Sparc.Blossom.Template.Database;

var builder = BlossomApplication.CreateBuilder(args);
builder.Services.AddScoped<Microsoft.AspNetCore.Authentication.AuthenticationService>();
var app = builder.Build();

// Add some random forecast data to the Blossom database (in-memory is built in, no need to hook up a DB to build your app!)
//using var scope = app.Services.CreateScope();
//var forecastRepository = scope.ServiceProvider.GetRequiredService<IRepository<Forecast>>();
//await forecastRepository.AddAsync(Forecast.Generate(60));

await app.RunAsync<Html>();
