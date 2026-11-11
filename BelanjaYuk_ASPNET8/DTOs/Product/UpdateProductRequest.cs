namespace BelanjaYuk.Dtos.Product
{
    public class UpdateProductRequest
    {
        public string ProductName { get; set; } = null!;
        public string ProductDesc { get; set; } = null!;
        public string IdCategory { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int Qty { get; set; }
        public List<string>? ProductImages { get; set; } // base64 image strings
    }
}