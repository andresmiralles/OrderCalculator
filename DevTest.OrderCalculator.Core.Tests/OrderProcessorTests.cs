using DevTest.OrderCalculator.Core.Helpers;
using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;
using DevTest.OrderCalculator.Core.Processors;
using DevTest.OrderCalculator.Core.Repositories;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace DevTest.OrderCalculator.Core.Tests
{
    public class OrderProcessorTests
    {
        private readonly Mock<IRepository<OrderSummary>> _orderSummaryRepositoryMock;
        private readonly OrderProcessor _orderProcessor;
        private readonly Order _order;
        private readonly Client _client;
        private readonly Promotion _promotion;
        private readonly Coupon _coupon;

        public OrderProcessorTests()
        {
            _orderSummaryRepositoryMock = new Mock<IRepository<OrderSummary>>();
            var fakeOrderRepository = new FakeOrderRepository(new ConsoleLogger());
            _order = fakeOrderRepository.Get(1);
            _client = _order.Client;
            _coupon = _order.Items.SingleOrDefault(i => i.Coupon?.Id == 1).Coupon;
            _promotion = _order.Items.FirstOrDefault(i => i.Promotion?.Id == 1).Promotion;

            _orderProcessor = new OrderProcessor(_order, _orderSummaryRepositoryMock.Object);
        }

        [Fact]
        public void ShouldThrowExceptionIfProcessorIsNull()
        {
            _client.Identifier = "Assurant";

            var exception = Assert.Throws<ArgumentNullException>(() => _orderProcessor.ProcessOrder());

            Assert.Contains("Identifier", exception.ParamName);
        }

        [Fact]
        public void ShouldAddAndSaveOrderSummary()
        {
            OrderSummary addedAndSavedOrderSummary = null;
            _orderSummaryRepositoryMock.Setup(r => r.Add(It.IsAny<OrderSummary>()))
                .Callback<OrderSummary>(orderSummary =>
                {
                    addedAndSavedOrderSummary = orderSummary;
                });

            _orderSummaryRepositoryMock.Setup(r => r.SaveChanges());

            var results = _orderProcessor.ProcessOrder();

            _orderSummaryRepositoryMock.Verify(r => r.Add(It.IsAny<OrderSummary>()), Times.Once);
            _orderSummaryRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);

            Assert.NotNull(addedAndSavedOrderSummary);
            Assert.Equal(results.PreTaxCost, addedAndSavedOrderSummary.PreTaxCost);
            Assert.Equal(results.TaxAmount, addedAndSavedOrderSummary.TaxAmount);
            Assert.Equal(results.TotalCost, addedAndSavedOrderSummary.TotalCost);
        }

        #region Tax After Discounts and No Luxury Exceptions (Default Behavior)

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscount()
        {
            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1120m, result.PreTaxCost);
            Assert.Equal(1198.4m, result.TotalCost);
            Assert.Equal(78.4m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountExpiredCoupon()
        {
            _coupon.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1280m, result.PreTaxCost);
            Assert.Equal(1369.6m, result.TotalCost);
            Assert.Equal(89.6m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountExpiredPromotion()
        {
            _promotion.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1400m, result.PreTaxCost);
            Assert.Equal(1498m, result.TotalCost);
            Assert.Equal(98m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountQuantity3()
        {
            _order.Items.SingleOrDefault(i => i.Id == 2).Quantity = 3;

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1280m, result.PreTaxCost);
            Assert.Equal(1369.6m, result.TotalCost);
            Assert.Equal(89.6m, result.TaxAmount);
        }
        #endregion

        #region Tax Before Discounts Behavior with No Luxury Exceptions
        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscount()
        {
            _client.Identifier = "FL";
            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1232m, result.TotalCost);
            Assert.Equal(112m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountExpiredCoupon()
        {
            _client.Identifier = "FL";
            _coupon.StartDate = DateTime.Now.AddDays(1);
            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1392m, result.TotalCost);
            Assert.Equal(112m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountExpiredPromotion()
        {
            _client.Identifier = "FL";
            _promotion.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1512m, result.TotalCost);
            Assert.Equal(112m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountQuantity3()
        {
            _client.Identifier = "FL";
            _order.Items.SingleOrDefault(i => i.Id == 2).Quantity = 3;

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1800m, result.PreTaxCost);
            Assert.Equal(1406m, result.TotalCost);
            Assert.Equal(126m, result.TaxAmount);
        }
        #endregion

        #region Tax After Discounts and With Luxury Exceptions
        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountLuxuryx2()
        {
            _client.Identifier = "NY";

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1120m, result.PreTaxCost);
            Assert.Equal(1243.2m, result.TotalCost);
            Assert.Equal(123.2m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountLuxuryx2ExpiredCoupon()
        {
            _client.Identifier = "NY";
            _coupon.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1280m, result.PreTaxCost);
            Assert.Equal(1425.6m, result.TotalCost);
            Assert.Equal(145.6m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountLuxuryx2ExpiredPromotion()
        {
            _client.Identifier = "NY";
            _promotion.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1400m, result.PreTaxCost);
            Assert.Equal(1554m, result.TotalCost);
            Assert.Equal(154m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryDefaultTaxAfterDiscountLuxuryx2Quantity3()
        {
            _client.Identifier = "NY";
            _order.Items.SingleOrDefault(i => i.Id == 2).Quantity = 3;

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1280m, result.PreTaxCost);
            Assert.Equal(1414.4m, result.TotalCost);
            Assert.Equal(134.4m, result.TaxAmount);
        }

        #endregion

        #region Tax Before Discounts and With Luxury Exceptions
        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountLuxuryx2()
        {
            _client.Identifier = "NM";

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1302m, result.TotalCost);
            Assert.Equal(182m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountLuxuryx2ExpiredCoupon()
        {
            _client.Identifier = "NM";
            _coupon.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1462m, result.TotalCost);
            Assert.Equal(182m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountLuxuryx2ExpiredPromotion()
        {
            _client.Identifier = "NM";
            _promotion.StartDate = DateTime.Now.AddDays(1);

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1600m, result.PreTaxCost);
            Assert.Equal(1582m, result.TotalCost);
            Assert.Equal(182m, result.TaxAmount);
        }

        [Fact]
        public void ShouldReturnExpectedOrderSummaryTaxBeforeDiscountLuxuryx2Quantity3()
        {
            _client.Identifier = "NM";
            _order.Items.SingleOrDefault(i => i.Id == 2).Quantity = 3;

            var result = _orderProcessor.ProcessOrder();

            Assert.NotNull(result);
            Assert.Equal(1800m, result.PreTaxCost);
            Assert.Equal(1476m, result.TotalCost);
            Assert.Equal(196m, result.TaxAmount);
        }
        #endregion
    }
}
