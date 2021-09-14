using DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators;
using DevTest.OrderCalculator.Core.Models;
using System;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class NYTaxCalculator : ClientTaxCalculatorBase
    {
        public NYTaxCalculator(Order order)
            : base(order,
                  new DefaultTaxAfterDiscountOrderCalculator(new TaxSettings { LuxuryTaxMultiplier = 2m }))
        {
        }
    }
}
