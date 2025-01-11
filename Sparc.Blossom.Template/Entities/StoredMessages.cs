namespace Sparc.Blossom.Template
{
    public class StoredMessages(BlossomAggregateOptions<StoredMessage> options) : BlossomAggregate<StoredMessage>(options)
    {
        public BlossomQuery<StoredMessage> AllStoredMessagesForUser(string userId)
            => Query().Where(x => x.UserId == userId);
    }
}