namespace Sparc.Blossom.Template
{
    public class Message : BlossomEntity<string>
    {
        public Message(string userId, StoredMessage message)
        {
            Id = new Guid().ToString();
            UserId = userId;
            MessageText = message.Text;
            MessageIcon = message.Icon;
            MessageType = message.Type;
            DateCreated = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string MessageText { get; set; }
        public string MessageIcon { get; set; }
        public MessageTypes MessageType { get; set; }
        // need something for message status -- keep track of if sent/delivered/read/read by who of their approved users, etc
        public DateTime DateCreated { get; set; }
    }
}