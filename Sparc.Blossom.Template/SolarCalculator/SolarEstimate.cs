
using Bogus;

namespace Sparc.Blossom.Girassol.Estimates;

public class SolarEstimate : BlossomEntity<string>
{
    public string Email { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
    public double AvgMonthlyElectricityBill { get; set; }
    public DateTime CreatedDate { get; set; }

    public SolarEstimate()
    {
         
    }
    public SolarEstimate(string email, string address, string type, double avgMonthlyElectricityBill)
    {
        Email = email;
        Address = address;
        Type = type;
        AvgMonthlyElectricityBill = avgMonthlyElectricityBill;
        CreatedDate = DateTime.UtcNow;
    }

    internal static List<SolarEstimate> Generate(int qty)
    {
        var faker = new Faker<SolarEstimate>()
            .RuleFor(o => o.Email, f => f.Internet.Email())
            .RuleFor(o => o.Address, f => f.Address.FullAddress())
            .RuleFor(o => o.Type, f => f.PickRandom(new[] { "Residential", "Commercial" }))
            .RuleFor(o => o.AvgMonthlyElectricityBill, f => f.Random.Double(50, 500))
            .RuleFor(o => o.CreatedDate, f => f.Date.Past());
            
        return faker.Generate(qty);
    }
}
