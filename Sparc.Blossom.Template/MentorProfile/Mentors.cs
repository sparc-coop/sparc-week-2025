namespace Sparc.Blossom.Template.Mentors;

public class Mentors(BlossomAggregateOptions<Mentor> options) : BlossomAggregate<Mentor>(options)
{
    public BlossomQuery<Mentor> GetMentorByUserId(string userId)
        => Query().Where(x => x.UserId == userId);

    public BlossomQuery<Mentor> GetAllMentors()
        => Query().OrderBy(x => x.Expertise);

    public BlossomQuery<Mentor> GetMentorsByExpertise(string expertise)
        => Query().Where(x => x.Expertise.Contains(expertise));
}
