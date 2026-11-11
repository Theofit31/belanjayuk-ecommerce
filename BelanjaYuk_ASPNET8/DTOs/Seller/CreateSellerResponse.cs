namespace BelanjaYuk.Dtos.Seller
{
    public class CreateSellerResponse
    {
        public string IdUserSeller { get; set; } = null!;
        public string SellerName { get; set; } = null!;
        public string SellerCode { get; set; } = null!;
        public string Message { get; set; } = "Toko berhasil dibuat.";
    }
}