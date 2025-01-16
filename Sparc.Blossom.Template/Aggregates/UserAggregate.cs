using Sparc.Blossom.Template.Entities;

namespace Sparc.Blossom.Template.Aggregates
{
    public class UserAggregate(BlossomAggregateOptions<User> options) : BlossomAggregate<User>(options)
    {
        public async Task<List<User>> GetUsersAsync() =>
            (await Query()
            .Execute())
            .ToList();
    }

}
