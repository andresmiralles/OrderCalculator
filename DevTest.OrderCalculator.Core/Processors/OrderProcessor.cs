using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;
using DevTest.OrderCalculator.Core.Repositories;

namespace DevTest.OrderCalculator.Core.Processors
{
    public class OrderProcessor
    {
        private readonly Order _order;
        private readonly IRepository<OrderSummary> _orderSummaryRepository;

        public OrderProcessor(Order order, IRepository<OrderSummary> orderSummaryRepository)
        {
            _order = order;
            _orderSummaryRepository = orderSummaryRepository;
        }

        public OrderSummary ProcessOrder()
        {
            var selector = new TaxCalculatorSelector();
            var taxCalculator = selector.Create(_order);

            var orderSummaryResults = taxCalculator.ProcessOrder();
            _orderSummaryRepository.Add(orderSummaryResults);
            _orderSummaryRepository.SaveChanges();

            return orderSummaryResults;
        }
    }
}
