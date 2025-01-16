using Sparc.Blossom.Template.UserProfile;

namespace Sparc.Blossom.Template.MentorsProfile;


public class Mentor : BlossomEntity<string>
{
    public string UserId { get; set; } 
    public List<string> Expertise { get; set; } = new List<string>();
    public List<string> AvailableTimes { get; set; } = new List<string>();

    public User User { get; set; }

    public Mentor() : base(Guid.NewGuid().ToString())
    {
    }

    public Mentor(string userId, List<string> expertise, List<string> availableTimes) : base(Guid.NewGuid().ToString())
    {
        UserId = userId;
        Expertise = expertise;
        AvailableTimes = availableTimes;
    }

    public void UpdateExpertise(List<string> newExpertise)
    {
        Expertise = newExpertise;
    }

    public void UpdateAvailableTimes(List<string> newTimes)
    {
        AvailableTimes = newTimes;
    }

    public void SetUser(User user)
    {
        User = user;
        UserId = user.Id;
    }

    public static List<Mentor> Generate(int count)
    {
        var random = new Random();

        var firstNames = new[] { "Alice", "Bob", "Charlie", "Diana", "Edward", "Fiona", "George", "Hannah", "Isaac", "Julia" };
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Hernandez" };

        var expertiseOptions = new[] { "Software Development", "Data Science", "Design", "Project Management", "Marketing" };
        var availableTimesOptions = new[] { "Morning", "Afternoon", "Evening" };

        var mentors = new List<Mentor>();

        for (int i = 0; i < count; i++)
        {
            var userId = Guid.NewGuid().ToString();

            var expertise = expertiseOptions
                .OrderBy(_ => random.Next())
                .Take(random.Next(1, expertiseOptions.Length))
                .ToList();

            var availableTimes = availableTimesOptions
                .OrderBy(_ => random.Next())
                .Take(random.Next(1, availableTimesOptions.Length))
                .ToList();

            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}@example.com";

            var mentor = new Mentor(userId, expertise, availableTimes);

            var user = new User(firstName, lastName, email);
            user.Id = userId;

            mentor.SetUser(user);

            mentors.Add(mentor);
        }

        return mentors;
    }

    public static List<string> GetTimeSlots(string period)
    {
        var slots = new List<string>();

        TimeSpan start, end;

        switch (period.ToLower())
        {
            case "morning":
                start = new TimeSpan(8, 0, 0); // 8:00 AM
                end = new TimeSpan(12, 0, 0); // 12:00 PM
                break;
            case "afternoon":
                start = new TimeSpan(13, 0, 0); // 1:00 PM
                end = new TimeSpan(17, 0, 0); // 5:00 PM
                break;
            case "evening":
                start = new TimeSpan(18, 0, 0); // 6:00 PM
                end = new TimeSpan(21, 0, 0); // 9:00 PM
                break;
            default:
                throw new ArgumentException("Invalid period");
        }

        var current = start;
        while (current + TimeSpan.FromMinutes(50) <= end)
        {
            slots.Add($"{current:h\\:mm tt} - {(current + TimeSpan.FromMinutes(50)):h\\:mm tt}");
            current += TimeSpan.FromMinutes(60); 
        }

        return slots;
    }

    public List<string> GetAvailableTimeSlots()
    {
        var allSlots = new List<string>();

        foreach (var period in AvailableTimes)
        {
            allSlots.AddRange(GetTimeSlots(period));
        }

        return allSlots;
    }
}

