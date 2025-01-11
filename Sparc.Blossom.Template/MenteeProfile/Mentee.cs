using Sparc.Blossom.Template.Users; 

namespace Sparc.Blossom.Template.Mentees;

public class Mentee : BlossomEntity<string>
{
    public string UserId { get; set; } 
    public List<string> Interests { get; set; }
    public List<string> PreferredTimes { get; set; }

    public Users.User User { get; private set; }

    public Mentee(string userId, List<string> interests, List<string> preferredTimes) : base(Guid.NewGuid().ToString())
    {
        UserId = userId;
        Interests = interests;
        PreferredTimes = preferredTimes;
    }

    public void UpdateInterests(List<string> newInterests)
    {
        Interests = newInterests;
    }

    public void UpdatePreferredTimes(List<string> newTimes)
    {
        PreferredTimes = newTimes;
    }

    public void SetUser(Users.User user)
    {
        User = user;
        UserId = user.Id;
    }
}
