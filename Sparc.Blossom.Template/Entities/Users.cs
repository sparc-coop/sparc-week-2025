namespace Sparc.Blossom.Template
{
    public class Users(BlossomAggregateOptions<User> options) : BlossomAggregate<User>(options)
    {
        public BlossomQuery<User> AllUsers()
            => Query().OrderBy(x => x.LastName);

        public BlossomQuery<User> AllPrimaryUsers()
            => Query().Where(x => x.UserType == UserTypes.Primary).OrderBy(x => x.LastName);

        public BlossomQuery<User> SecondaryUsers()
            => Query().Where(x => x.UserType == UserTypes.Secondary).OrderBy(x => x.LastName);

        //public BlossomQuery<User> GetApprovedUsers(string userId)
        //{
        //    var user = Query().Where(x => x.Id == userId);
        //    user ?ApprovedUsers.ToList().OrderBy(x => x.LastName);
        //}
    }
}