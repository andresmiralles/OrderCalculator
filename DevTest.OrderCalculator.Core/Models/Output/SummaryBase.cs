namespace DevTest.OrderCalculator.Core.Models.Output
{
    public abstract class SummaryBase : EntityBase
    {
        public decimal TotalCost { get; set; }
        public decimal PreTaxCost { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
