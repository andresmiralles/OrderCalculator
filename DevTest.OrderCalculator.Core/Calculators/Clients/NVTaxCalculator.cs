using DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators;
using DevTest.OrderCalculator.Core.Models;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class NVTaxCalculator : ClientTaxCalculatorBase
    {
        public NVTaxCalculator(Order order)
            : base(order,
                  new TaxBeforeDiscountOrderItemCalculator())
        {
        }
    }
}
