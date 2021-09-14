using DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators;
using DevTest.OrderCalculator.Core.Models;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class NMTaxCalculator : ClientTaxCalculatorBase
    {
        public NMTaxCalculator(Order order)
            : base(order,
                  new TaxBeforeDiscountOrderItemCalculator(new TaxSettings { LuxuryTaxMultiplier = 2m }))
        {
        }
    }
}
