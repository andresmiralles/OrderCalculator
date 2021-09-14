using DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators;
using DevTest.OrderCalculator.Core.Models;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class FLTaxCalculator : ClientTaxCalculatorBase
    {
        public FLTaxCalculator(Order order)
            : base(order,
                  new TaxBeforeDiscountOrderItemCalculator())
        {
        }
    }
}
