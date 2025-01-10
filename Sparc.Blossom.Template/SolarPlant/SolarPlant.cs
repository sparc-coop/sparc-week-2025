namespace Sparc.Blossom.Template.SolarPlant;

public class SolarPlant : BlossomEntity<string>
{
    public string Name { get; set; }        
    public string Location { get; set; }   
    public double CapacityMW { get; set; } 
    public DateTime CommissionedDate { get; set; }
    public string Owner { get; set; }      
}
