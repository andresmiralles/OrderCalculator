using System;

namespace DevTest.OrderCalculator.Core.Models
{
    abstract public class DiscountBase : EntityBase
    {
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsValid => DateTime.Now > StartDate
            && DateTime.Now < EndDate
            && DiscountPercentage > 0m
            && DiscountPercentage < 1m;
    }
}
