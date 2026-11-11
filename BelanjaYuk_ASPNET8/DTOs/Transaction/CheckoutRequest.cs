namespace BelanjaYuk.Dtos.Transaction
{
    public class CheckoutRequest
    {
        public string UserId { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public decimal? TotalPrice { get; set; }

        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public string ProductId { get; set; } = null!;
        public int Qty { get; set; }
        public decimal? Price { get; set; }
    }
}