namespace Sparc.Blossom.Template.Mentees;

public class Mentees(BlossomAggregateOptions<Mentee> options) : BlossomAggregate<Mentee>(options)
{
    public BlossomQuery<Mentee> GetMenteeByUserId(string userId)
        => Query().Where(x => x.UserId == userId);

    public BlossomQuery<Mentee> GetAllMentees()
        => Query().OrderBy(x => x.Interests);

    public BlossomQuery<Mentee> GetMenteesByInterests(string interests)
        => Query().Where(x => x.Interests.Contains(interests));
}