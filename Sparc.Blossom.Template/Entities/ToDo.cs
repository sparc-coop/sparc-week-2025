namespace Sparc.Blossom.Template
{
    public class ToDo : BlossomEntity<string>
    {
        public ToDo(string userId, string title, string description, TodoTypes type, TodoStatus status)
        {
            Id = new Guid().ToString();
            UserId = userId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            DateCreated = DateTime.UtcNow;
        }

        public ToDo(string id, string userId, string title, string description, TodoTypes type, TodoStatus status)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            DateModified = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoTypes Type { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

    public enum TodoTypes
    {
        Medication,
        Appointment,
        Misc
    }

    public enum TodoStatus
    {
        Upcoming,
        Pending,
        Completed,
        Overdue
    }

    public enum TodoFrequency
    {
        Once,
        Daily,
        Weekly,
        Monthly,
    }
}