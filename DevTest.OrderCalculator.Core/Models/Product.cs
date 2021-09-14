namespace DevTest.OrderCalculator.Core.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public ProductCategory Category { get; set; }
    }
}