using Sparc.Blossom.Template.MentorsProfile;
using Sparc.Blossom.Template.MenteesProfile;

namespace Sparc.Blossom.Template.Users;

public class User : BlossomEntity<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }

    public Mentor? MentorProfile { get; private set; }
    public Mentee? MenteeProfile { get; private set; }

    public User() : base(Guid.NewGuid().ToString()) 
    { 
    }

    public User(string firstName, string lastName, string email) : base(Guid.NewGuid().ToString())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }    

    public void AssignMentorProfile(Mentor mentor)
    {
        MentorProfile = mentor;
    }

    public void AssignMenteeProfile(Mentee mentee)
    {
        MenteeProfile = mentee;
    }
}