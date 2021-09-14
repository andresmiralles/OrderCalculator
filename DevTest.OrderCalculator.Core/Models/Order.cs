using DevTest.OrderCalculator.Core.Models.Output;
using System.Collections.Generic;

namespace DevTest.OrderCalculator.Core.Models
{
    public class Order : EntityBase
    {
        public Order()
        {
            OrderSummary = new OrderSummary();
        }

        public IEnumerable<OrderItem> Items { get; set; }
        public Client Client { get; set; }

        public OrderSummary OrderSummary { get; set; }
    }
}
