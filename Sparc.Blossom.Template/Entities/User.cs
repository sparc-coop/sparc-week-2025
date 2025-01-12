namespace Sparc.Blossom.Template.Entities;

public class User : BlossomEntity<Guid>
{
    // Parameterless constructor for form binding
    public User() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = DateTime.UtcNow;
    }

    // Constructor with parameters to set properties for backend use
    public User(string firstName, string lastName, string email, string phone, Sparc.Blossom.Template.Common.UserType userType)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        UserType = userType;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Sparc.Blossom.Template.Common.UserType UserType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}
