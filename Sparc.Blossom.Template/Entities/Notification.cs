namespace Sparc.Blossom.Template
{
    public class Notification : BlossomEntity
    {
        public Notification(string userId, string taskId, string status)
        {
            Id = new Guid().ToString();
            UserId = userId;
            TaskId = taskId;

            DateCreated = DateTime.UtcNow;
        }
        public Notification(string id, string userId, string taskId, NotificationStatus status)
        {
            Id = id;
            UserId = userId;
            TaskId = taskId;
            Status = status;
            if (status == NotificationStatus.Dismissed)
            {
                DateModified = DateTime.UtcNow;
            }
            else
            {
                DateCreated = DateTime.UtcNow;
            }
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public enum NotificationStatus
    {
        Active,
        Dismissed
    }
}