namespace Sparc.Blossom.Template.Users;

public class Users(BlossomAggregateOptions<User> options) : BlossomAggregate<User>(options)
{
    public BlossomQuery<User> GetUserById(string userId)
        => Query().Where(x => x.Id == userId);

    public BlossomQuery<User> GetUserByEmail(string email)
        => Query().Where(x => x.Email == email);

    public BlossomQuery<User> GetAllUsers()
        => Query().OrderBy(x => x.FullName);
}
