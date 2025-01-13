using Sparc.Blossom.Template.Users;

namespace Sparc.Blossom.Template.MentorsProfile;


public class Mentor : BlossomEntity<string>
{
    public string UserId { get; set; } 
    public List<string> Expertise { get; set; }
    public List<string> AvailableTimes { get; set; }

    public User User { get; private set; }

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
}

