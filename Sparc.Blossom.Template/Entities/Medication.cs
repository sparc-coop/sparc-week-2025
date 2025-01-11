namespace Sparc.Blossom.Template
{
    public class Medication : BlossomEntity
    {
        public Medication(string userId, string name, string description, string dosage, string frequency, string instructions, string prescribingMD, DateTime refillBy)
        {
            Id = new Guid().ToString();
            UserId = userId;
            Name = name;
            Description = description;
            Dosage = dosage;
            Frequency = frequency;
            Instructions = instructions;
            PrescribingMD = prescribingMD;
            RefillBy = refillBy;
            DateCreated = DateTime.UtcNow;
        }
        public Medication(string id, string userId, string name, string dosage, string frequency, string description, string instructions, string prescribingMD, DateTime refillBy)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            Dosage = dosage;
            Frequency = frequency;
            Instructions = instructions;
            PrescribingMD = prescribingMD;
            RefillBy = refillBy;
            DateModified = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string PrescribingMD { get; set; }
        public DateTime RefillBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}