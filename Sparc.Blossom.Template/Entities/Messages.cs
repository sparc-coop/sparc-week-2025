namespace Sparc.Blossom.Template
{
    public class Messages(BlossomAggregateOptions<Message> options) : BlossomAggregate<Message>(options)
    {
        public BlossomQuery<Message> AllUserMessages(string userId)
            => Query().Where(x => x.UserId == userId);

        public BlossomQuery<Message> AllUserUpdates(string userId)
        {
            var message = Query().Where(x => x.UserId == userId);
            return message?.Where(x => x.MessageType == MessageTypes.Update);
        }

        public BlossomQuery<Message> AllUserAlerts(string userId)
        {
            var message = Query().Where(x => x.UserId == userId);
            return message?.Where(x => x.MessageType == MessageTypes.Alert);
        }

        public BlossomQuery<Message> AllUserEmergencies(string userId)
        {
            var message = Query().Where(x => x.UserId == userId);
            return message?.Where(x => x.MessageType == MessageTypes.Emergency);
        }

        //public BlossomQuery<Message> UnreadMessages(string userId)
        //{
        //    var message = Query().Where(x => x.UserId == userId);
        //    return message?.Where(x => x.MessageStatus == MessageStatus.Unread);
        //}
    }
}