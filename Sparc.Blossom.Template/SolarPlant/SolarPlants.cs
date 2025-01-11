namespace Sparc.Blossom.Girassol.SolarPlant;

public class SolarPlants(BlossomAggregateOptions<SolarPlant> options) : BlossomAggregate<SolarPlant>(options)
{
    public BlossomQuery<SolarPlant> Filter(
        string? name = null,
        string? location = null,
        double? minCapacity = null,
        double? maxCapacity = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int? page = 1,
        int? perPage = 10)
    {
        var query = Query();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        //TODO by location or lat long?

        if (minCapacity.HasValue)
        {
            query = query.Where(x => x.CapacityMW >= minCapacity.Value);
        }
        if (maxCapacity.HasValue)
        {
            query = query.Where(x => x.CapacityMW <= maxCapacity.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.CommissionedDate >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            query = query.Where(x => x.CommissionedDate <= endDate.Value);
        }

        return query.SkipTake(page.Value * perPage.Value, perPage.Value);
    }
}
