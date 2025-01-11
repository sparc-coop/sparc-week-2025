namespace Sparc.Blossom.Template
{
    public class User : BlossomEntity
    {
        public User(string firstName, string lastName, string displayName, string email, string phone, UserTypes userType)
        {
            Id = new Guid().ToString();
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Email = email;
            Phone = phone;
            UserType = userType;
            DateCreated = DateTime.UtcNow;
        }

        public User(string id, string firstName, string lastName, string displayName, string email, string phone, UserTypes userType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Email = email;
            Phone = phone;
            UserType = userType;
            DateModified = DateTime.UtcNow;
        }

        public User(string id, List<User> approvedUsers)
        {
            Id = id;
            ApprovedUsers = approvedUsers;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public List<User> ApprovedUsers { get; set; }
    }
    public enum UserTypes
    {
        Primary,
        Secondary
    }
}