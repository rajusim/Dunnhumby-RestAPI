namespace Dunnhumby.RestApi.Models
{
    public class ProductDto
    {
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
