namespace OrderCalculatorApp.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public required List<Product> Products { get; set; }
        public string? PromotionCode { get; set; }
        public required string ClientCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
