namespace DevTest.OrderCalculator.Core.Models
{
    public class TaxSettings
    {
        public decimal TaxRate { get; set; } = 0.07m;
        public decimal LuxuryTaxMultiplier { get; set; } = 1m;
    }
}