using Bogus;

namespace Sparc.Blossom.Girassol.SolarPlants;

public class SolarPlant : BlossomEntity<string>
{
    public string Name { get; set; }
    public string Location { get; set; }
    public double CapacityMW { get; set; }
    public DateTime CommissionedDate { get; set; }
    public List<SolarPlantImage> Images { get; set; }
    public string Owner { get; set; }

    public SolarPlant(string name, string location, double capacityMW, DateTime commissionedDate, string owner)
    {
        Name = name;
        Location = location;
        CapacityMW = capacityMW;
        CommissionedDate = commissionedDate;
        Owner = owner;
        Images = new List<SolarPlantImage>();
    }

    internal static List<SolarPlant> Generate(int qty)
    {
        var faker = new Faker<SolarPlant>()
            .CustomInstantiator(f => new SolarPlant(
                f.Company.CompanyName(),
                f.Address.City(),
                f.Random.Double(1, 100),
                f.Date.Past(20),
                f.Person.FullName))
            .RuleFor(sp => sp.Images, f => new List<SolarPlantImage>
            {
                new SolarPlantImage
                {
                    Id = f.Random.Guid().ToString(),
                    Url = $"imgs/solar-plants/{f.Random.Int(1,14)}.jpg",
                    Profile = f.Random.Bool(),
                    UploadDate = f.Date.Past()
                }
            });

        return faker.Generate(qty);
    }
}
