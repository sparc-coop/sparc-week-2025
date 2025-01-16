namespace Sparc.Blossom.Template
{
    public class StoredMessage : BlossomEntity<string>
    {
        public StoredMessage(string userId, string text, string iconPath, MessageTypes type)
        {
            Id = new Guid().ToString();
            UserId = userId;
            Text = text;
            Icon = iconPath;
            Type = type;
            DateCreated = DateTime.UtcNow;
        }

        public StoredMessage(string id, string userId, string text, string iconPath, MessageTypes type)
        {
            Id = id;
            UserId = userId;
            Text = text;
            Icon = iconPath;
            Type = type;
            DateModified = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public MessageTypes Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
    public enum MessageTypes
    {
        Update,
        Alert,
        Emergency
    }
}