namespace Sparc.Blossom.Template.Estimates;

public class SolarEstimate : BlossomEntity<string>
{
    public string Email { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
    public double AvgMonthlyEletricityBill { get; set; }

    public SolarEstimate(string email, string address, string type, double avgMonthlyElectricityBill)
    {
        Email = email;
        Address = address;
        Type = type;
        AvgMonthlyEletricityBill = avgMonthlyElectricityBill;
    }
}
