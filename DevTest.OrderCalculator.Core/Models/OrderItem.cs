using DevTest.OrderCalculator.Core.Models.Output;

namespace DevTest.OrderCalculator.Core.Models
{
    public class OrderItem : EntityBase
    {
        public OrderItem()
        {
            ItemDetailSummary = new ItemDetailSummary();
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Coupon Coupon { get; set; }
        public Promotion Promotion { get; set; }

        public ItemDetailSummary ItemDetailSummary { get; set; }
    }
}