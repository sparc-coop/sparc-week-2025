namespace Sparc.Blossom.Template.Estimates;

public class SolarEstimates(BlossomAggregateOptions<SolarEstimate> options) : BlossomAggregate<SolarEstimate>(options)
{
    public BlossomQuery<SolarEstimate> Filter(
        string? email = null,
        string? type = null,
        double? minBill = null,
        double? maxBill = null,
        int? page = 1,
        int? perPage = 10)
    {
        var query = Query();

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(x => x.Email.Contains(email));
        }

        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(x => x.Type == type);
        }

        if (minBill.HasValue)
        {
            query = query.Where(x => x.AvgMonthlyEletricityBill >= minBill.Value);
        }

        if (maxBill.HasValue)
        {
            query = query.Where(x => x.AvgMonthlyEletricityBill <= maxBill.Value);
        }

        page ??= 1;
        perPage ??= 10;

        return query.SkipTake(page.Value * perPage.Value, perPage.Value); ;
    }
}
