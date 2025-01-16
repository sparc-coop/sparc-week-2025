namespace Sparc.Blossom.Template.MentorsProfile;

public class Mentors(BlossomAggregateOptions<Mentor> options) : BlossomAggregate<Mentor>(options)
{
    public BlossomQuery<Mentor> GetMentorByUserId(string userId)
        => Query().Where(x => x.UserId == userId);

    public BlossomQuery<Mentor> GetAllMentors()
        => Query().OrderBy(x => x.Expertise);

    public BlossomQuery<Mentor> GetMentorsByExpertise(string expertise)
        => Query().Where(x => x.Expertise.Contains(expertise));

    public BlossomQuery<Mentor> GetMentorsByInterests(List<string> interests, string currentUserId)
    {
        return Query().Where(mentor => mentor.Expertise.Any(expertise => interests.Contains(expertise))
        && mentor.UserId != currentUserId);
    }
}
