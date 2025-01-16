using WhatsUpDocx;
using WhatsUpDocx.Services;

var builder = BlossomApplication.CreateBuilder(args);

builder.Services.AddHttpClient<AssistantsApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/");
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("API key for OpenAI is not configured.");
    }
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
    client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
});

var app = builder.Build();

await app.RunAsync<Html>();
