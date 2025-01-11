namespace Sparc.Blossom.Girassol.SolarPlant;

public class SolarPlant : BlossomEntity<string>
{
    public string Name { get; set; }        
    public string Location { get; set; }   
    public double CapacityMW { get; set; } 
    public DateTime CommissionedDate { get; set; }
    public string Owner { get; set; }

    public SolarPlant(string name, string location, double capacityMW, DateTime commissionedDate, string owner)
    {
        Name = name;
        Location = location;
        CapacityMW = capacityMW;
        CommissionedDate = commissionedDate;
        Owner = owner;
    }
}
