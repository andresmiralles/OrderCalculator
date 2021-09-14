using DevTest.OrderCalculator.Core.Models;
using DevTest.OrderCalculator.Core.Models.Output;

namespace DevTest.OrderCalculator.Core.Calculators.OrderItemCalculators
{
    public class TaxBeforeDiscountOrderItemCalculator : OrderItemCalculatorBase
    {
        public TaxBeforeDiscountOrderItemCalculator(TaxSettings taxSettings = default) : base (taxSettings)
        {
        }

        public override ItemSummary GenerateSummary(OrderItem item)
        {
            var basePrice = item.Product.BasePrice * item.Quantity;
            var preTaxCost = basePrice;
            var taxAmount = GetTaxAmount(item, preTaxCost);
            var afterTaxCost = preTaxCost + taxAmount;
            var couponDiscountedPrice = ApplyCoupon(item.Coupon, afterTaxCost, preTaxCost);
            var preTaxAfterCouponPrice = ApplyCoupon(item.Coupon, basePrice, basePrice);
            var promotionDiscountedPrice = ApplyPromotion(item.Promotion, couponDiscountedPrice, preTaxAfterCouponPrice);

            var totalDiscounts = afterTaxCost - promotionDiscountedPrice;
            var totalCost = afterTaxCost - totalDiscounts;

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
    }
}
