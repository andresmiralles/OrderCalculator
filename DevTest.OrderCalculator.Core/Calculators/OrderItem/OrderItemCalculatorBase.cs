using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;
using System.Collections.Generic;
using System.Linq;

namespace DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators
{
    public abstract class OrderItemCalculatorBase : IOrderItemCalculator
    {
        protected readonly TaxSettings _taxSettings;

        public OrderItemCalculatorBase(TaxSettings taxSettings = default)
        {
            _taxSettings = taxSettings ?? new TaxSettings();
        }

        public virtual OrderSummary GenerateSummary(Order order)
        {
            var itemSummaries = new List<ItemSummary>();

            foreach (var item in order.Items)
            {
                itemSummaries.Add(GenerateSummary(item));
            }

            order.OrderSummary.PreTaxCost = itemSummaries.Sum(s => s.PreTaxCost);
            order.OrderSummary.TaxAmount = itemSummaries.Sum(s => s.TaxAmount);
            order.OrderSummary.TotalCost = itemSummaries.Sum(s => s.TotalCost);

            return order.OrderSummary;
        }

        public virtual ItemSummary GenerateSummary(OrderItem item)
        {
            var basePrice = item.Product.BasePrice * item.Quantity;
            var couponDiscountedPrice = ApplyCoupon(item.Coupon, basePrice, basePrice);
            var promotionDiscountedPrice = ApplyPromotion(item.Promotion, couponDiscountedPrice, couponDiscountedPrice);
            var totalDiscounts = basePrice - promotionDiscountedPrice;
            var preTaxCost = promotionDiscountedPrice;
            var taxAmount = GetTaxAmount(item, preTaxCost);
            var afterTaxCost = preTaxCost + taxAmount;
            var totalCost = afterTaxCost;

            item.ItemDetailSummary = new ItemDetailSummary
            {
                BasePrice = basePrice,
                CouponDiscountedPrice = couponDiscountedPrice,
                PromotionDiscountedPrice = promotionDiscountedPrice,
                TotalDiscounts = totalDiscounts,
                PreTaxCost = preTaxCost,
                TaxAmount = taxAmount,
                AfterTaxCost = afterTaxCost,
                TotalCost = totalCost
            };

            return new ItemSummary
            {
                PreTaxCost = item.ItemDetailSummary.PreTaxCost,
                TaxAmount = item.ItemDetailSummary.TaxAmount,
                TotalCost = item.ItemDetailSummary.TotalCost
            };
        }

        protected virtual decimal ApplyCoupon(Coupon coupon, decimal priceToApplyCouponTo, decimal priceToBaseCouponOn)
        {
            return ApplyDiscount(coupon, priceToApplyCouponTo, priceToBaseCouponOn);
        }

        protected virtual decimal ApplyPromotion(Promotion promotion, decimal priceToApplyPromotionTo, decimal priceToBasePromotionOn)
        {
            return ApplyDiscount(promotion, priceToApplyPromotionTo, priceToBasePromotionOn);
        }

        protected virtual decimal GetTaxAmount(OrderItem item, decimal priceToApplyTaxTo)
        {
            // We can use this for luxury for now because it's just one easy rule.
            // If many other changes come in, we need create aditional OrderItem Calculator classes
            return priceToApplyTaxTo * _taxSettings.TaxRate * (item.Product.Category == ProductCategory.LuxuryItem ? _taxSettings.LuxuryTaxMultiplier : 1m);
        }

        private static decimal ApplyDiscount(DiscountBase discount, decimal priceToApplyDiscountTo, decimal priceToBaseDiscountOn)
        {
            return priceToApplyDiscountTo - (priceToBaseDiscountOn * DiscountAmount(discount));
        }

        private static decimal DiscountAmount(DiscountBase discount)
        {
            return discount is not null && discount.IsValid ? discount.DiscountPercentage : 0;
        }
    }
}
