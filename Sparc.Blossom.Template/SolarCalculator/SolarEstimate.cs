namespace Sparc.Blossom.Template.SolarCalculator;

public class SolarEstimate : BlossomEntity<string>
{
    public string Email { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
    public double AvgMonthlyEletricityBill { get; set; }
}
