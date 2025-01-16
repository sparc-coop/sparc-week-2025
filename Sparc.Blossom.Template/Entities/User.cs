using System.ComponentModel.DataAnnotations;

namespace Sparc.Blossom.Template.Entities
{
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

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        public Sparc.Blossom.Template.Common.UserType UserType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModifiedAt { get; set; }
    }
}
