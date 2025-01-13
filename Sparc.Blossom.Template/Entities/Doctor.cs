using System.ComponentModel.DataAnnotations;

namespace Sparc.Blossom.Template.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Specialty is required")]
        [StringLength(50, ErrorMessage = "Specialty can't be longer than 50 characters.")]
        public string Specialty { get; set; }
    }
}
