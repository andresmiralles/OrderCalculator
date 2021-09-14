using DevTest.OrderCalculator.Core.Models;

namespace DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators
{
    public class DefaultTaxAfterDiscountOrderCalculator : OrderItemCalculatorBase
    {
        public DefaultTaxAfterDiscountOrderCalculator(TaxSettings taxSettings = default) : base(taxSettings)
        {
        }
    }
}
