namespace DevTest.OrderCalculator.Core.Models.Output
{
    public abstract class DetailSummaryBase : SummaryBase
    {
        public decimal BasePrice { get; set; }
        public decimal CouponDiscountedPrice { get; set; }
        public decimal PromotionDiscountedPrice { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal AfterTaxCost { get; set; }
    }
}