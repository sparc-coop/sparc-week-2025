using System.ComponentModel.DataAnnotations;

namespace Sparc.Blossom.Template.Entities
{
    public class Doctor : BlossomEntity<Guid>
    {

        public Doctor() : base(Guid.NewGuid())
        {
            CreatedAt = DateTime.UtcNow;
            LastModifiedAt = DateTime.UtcNow;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
