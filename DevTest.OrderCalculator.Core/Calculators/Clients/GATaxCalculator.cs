using DevTest.OrderCalculator.Core.Models;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public class GATaxCalculator : ClientTaxCalculatorBase
    {
        public GATaxCalculator(Order order)
            : base(order)
        {
        }
    }
}
