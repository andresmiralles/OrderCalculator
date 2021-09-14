using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;

namespace DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators
{
    public interface IOrderItemCalculator
    {
        OrderSummary GenerateSummary(Order order);
        ItemSummary GenerateSummary(OrderItem item);
    }
}