using WhatsUpDocx;
using WhatsUpDocx.Services;

var builder = BlossomApplication.CreateBuilder(args);

builder.Services.AddHttpClient<AssistantsApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/");
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "key_goes_here");
    client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
});

var app = builder.Build();

// Add some random forecast data to the Blossom database (in-memory is built in, no need to hook up a DB to build your app!)
//using var scope = app.Services.CreateScope();
//var forecastRepository = scope.ServiceProvider.GetRequiredService<IRepository<Forecast>>();
//await forecastRepository.AddAsync(Forecast.Generate(60));

await app.RunAsync<Html>();
