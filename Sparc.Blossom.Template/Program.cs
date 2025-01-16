using Sparc.Blossom.Girassol;
using Sparc.Blossom.Girassol.Estimates;
using Sparc.Blossom.Girassol.SolarPlants;

var builder = BlossomApplication.CreateBuilder(args);

var app = builder.Build();

// Add some random forecast data to the Blossom database (in-memory is built in, no need to hook up a DB to build your app!)
using var scope = app.Services.CreateScope();
var forecastRepository = scope.ServiceProvider.GetRequiredService<IRepository<Forecast>>();
await forecastRepository.AddAsync(Forecast.Generate(60));

var estimateRepository = scope.ServiceProvider.GetRequiredService<IRepository<SolarEstimate>>();
await estimateRepository.AddAsync(SolarEstimate.Generate(60));

var solarPlantRepository = scope.ServiceProvider.GetRequiredService<IRepository<SolarPlant>>();
await solarPlantRepository.AddAsync(SolarPlant.Generate(60));

await app.RunAsync<Html>();
 