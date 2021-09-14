using DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators;
using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;

namespace DevTest.OrderCalculator.Core.Calculators.Clients
{
    public abstract class ClientTaxCalculatorBase
    {
        protected readonly Order _order;
        protected readonly IOrderItemCalculator _orderCalculator;

        public ClientTaxCalculatorBase(Order order, IOrderItemCalculator orderCalculator = default)
        {
            _order = order;
            _orderCalculator = orderCalculator ?? new DefaultTaxAfterDiscountOrderCalculator(new TaxSettings());
        }

        public virtual OrderSummary ProcessOrder()
        {
            return _orderCalculator.GenerateSummary(_order);
        }
    }
}
