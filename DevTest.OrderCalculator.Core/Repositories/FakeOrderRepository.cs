using DevTest.OrderCalculator.Core.Helpers;
using DevTest.OrderCalculator.Core.Models;
using System;
using System.Collections.Generic;

namespace DevTest.OrderCalculator.Core.Repositories
{
    public class FakeOrderRepository : GenericFakeRepository<Order>
    {
        public FakeOrderRepository(ILogger logger) : base(logger)
        {
        }

        public override Order Get(long id)
        {
            var promotion = new Promotion { Id = 1, Name = "Labor Day Promotion", DiscountPercentage = 0.2m, StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(10) };
            var coupon = new Coupon { Id = 1, Code = "XX33", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3), DiscountPercentage = 0.2m };

            return new Order
            {
                Id = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        Product = new Product { Id = 1, Name = "Smartphone", BasePrice = 1000m, Category = ProductCategory.LuxuryItem },
                        Quantity = 1,
                        Promotion = promotion,
                        Coupon = coupon,
                    },
                    new OrderItem
                    {
                        Id = 2,
                        Product = new Product { Id = 2, Name = "Chair", BasePrice = 100m },
                        Quantity = 1,
                        Promotion = promotion,
                    },
                    new OrderItem
                    {
                        Id = 3,
                        Product = new Product { Id = 3, Name = "Desk", BasePrice = 500m },
                        Quantity = 1,
                        Promotion = promotion,
                    }
                },
                Client = new Client { Id = 1, Identifier = "GA" }
            };
        }
    }
}
