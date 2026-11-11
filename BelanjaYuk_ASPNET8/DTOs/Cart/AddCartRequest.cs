namespace BelanjaYuk.Dtos.Cart
{
    public class AddCartRequest
    {
        public string IdUser { get; set; } = null!;
        public string IdProduct { get; set; } = null!;
        public int Qty { get; set; }
    }
}