using DevTest.OrderCalculator.Core.Calculators.Clients;
using DevTest.OrderCalculator.Core.Models;
using System;

namespace DevTest.OrderCalculator.Core.Processors
{
    public class TaxCalculatorSelector
    {
        public ClientTaxCalculatorBase Create(Order order)
        {
            try
            {
                return (ClientTaxCalculatorBase)Activator.CreateInstance(
                    Type.GetType($"DevTest.OrderCalculator.Core.Calculators.Clients.{order.Client.Identifier}TaxCalculator"),
                        new object[] { order });
            }
            catch
            {
                return new NotSupportedClientTaxCalculator(order);
            }
        }
    }
}
