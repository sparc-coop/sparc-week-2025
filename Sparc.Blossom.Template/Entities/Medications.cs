namespace Sparc.Blossom.Template
{
    public class Medications(BlossomAggregateOptions<Medication> options) : BlossomAggregate<Medication>(options)
    {
        public BlossomQuery<Medication> AllMedsForUser(string userId)
            => Query().Where(x => x.UserId == userId);

        public BlossomQuery<Medication> DailyMedsForUser(string userId)
        {
            var userMeds = Query().Where(x => x.UserId == userId);
            return userMeds?.Where(x => x.Frequency == "daily");
        }

        public BlossomQuery<Medication> NeedRefill(string userId)
        {
            var userMeds = Query().Where(x => x.UserId == userId);
            var medsNeedRefill = Query().Where(x => x.RefillBy <= DateTime.UtcNow);
            return medsNeedRefill?.OrderBy(x => x.RefillBy);
        }
    }
}