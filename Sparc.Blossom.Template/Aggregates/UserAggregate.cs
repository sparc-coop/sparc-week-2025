using Sparc.Blossom.Template.Entities;

namespace Sparc.Blossom.Template.Aggregaters
{
    public class UserAggregate(BlossomAggregateOptions<User> options) : BlossomAggregate<User>(options)
    {
        public async Task<List<User>> GetAllDoctorsAsync() =>
            (await Query().Where(x => x.UserType == Common.UserType.Doctor)
            .Execute())
            .ToList();
    }

}
