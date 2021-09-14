using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;
using System;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class NotSupportedClientTaxCalculator : ClientTaxCalculatorBase
    {
        public NotSupportedClientTaxCalculator(Order order) : base(order)
        {
        }

        public override OrderSummary ProcessOrder()
        {
            Console.WriteLine("Provided Client is not yet supported on our component. TaxCalculator.");
            throw new ArgumentNullException(nameof(_order.Client.Identifier));
        }
    }
}
