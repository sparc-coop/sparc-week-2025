using Sparc.Blossom.Template;
using Sparc.Blossom.Template.MentorsProfile;
using Sparc.Blossom.Template.UserProfile;


var builder = BlossomApplication.CreateBuilder(args);

builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<MentorService>();

var app = builder.Build();

// Add some random forecast data to the Blossom database (in-memory is built in, no need to hook up a DB to build your app!)
using var scope = app.Services.CreateScope();
var forecastRepository = scope.ServiceProvider.GetRequiredService<IRepository<Forecast>>();
await forecastRepository.AddAsync(Forecast.Generate(60));

var mentorRepository = scope.ServiceProvider.GetRequiredService<IRepository<Mentor>>();
var mentors = Mentor.Generate(50);
foreach (var mentor in mentors)
{
    await mentorRepository.AddAsync(mentor);
}

await app.RunAsync<Html>();
