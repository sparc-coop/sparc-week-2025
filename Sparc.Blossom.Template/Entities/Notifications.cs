namespace Sparc.Blossom.Template
{
    public class Notifications(BlossomAggregateOptions<Notification> options) : BlossomAggregate<Notification>(options)
    {
        public BlossomQuery<Notification> AllNotificationsForUser(string userId)
            => Query().Where(x => x.UserId == userId);

        public BlossomQuery<Notification> ActiveUserNotifications(string userId)
        {
            var userNotifications = Query().Where(x => x.UserId == userId);
            return userNotifications?.Where(x => x.Status == NotificationStatus.Active);
        } 
    }    
}