using Sparc.Blossom.Template.UserProfile;

namespace Sparc.Blossom.Template.MenteesProfile;

public class Mentee : BlossomEntity<string>
{
    public string UserId { get; set; } 
    public List<string> Interests { get; set; } = new List<string>();
    public List<string> PreferredTimes { get; set; } = new List<string>();

    public User User { get; private set; }

    public string AssignedMentorId { get; private set; }

    public Mentee() : base(Guid.NewGuid().ToString())
    {
    }

    public Mentee(string userId, List<string> interests, List<string> preferredTimes) : base(Guid.NewGuid().ToString())
    {
        UserId = userId;
        Interests = interests;
        PreferredTimes = preferredTimes;
    }

    public void AssignMentor(string mentorId)
    {
        if (string.IsNullOrWhiteSpace(mentorId))
        {
            throw new ArgumentException("Mentor ID cannot be null or empty.", nameof(mentorId));
        }

        AssignedMentorId = mentorId;
    }

    public void UpdateInterests(List<string> newInterests)
    {
        Interests = newInterests;
    }

    public void UpdatePreferredTimes(List<string> newTimes)
    {
        PreferredTimes = newTimes;
    }

    public void SetUser(User user)
    {
        User = user;
        UserId = user.Id;
    }
}
