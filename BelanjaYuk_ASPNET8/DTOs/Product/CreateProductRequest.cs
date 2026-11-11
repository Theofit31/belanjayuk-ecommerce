using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.Dtos.Product
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Nama produk wajib diisi.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Nama produk minimal 3 karakter.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Deskripsi produk wajib diisi.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Deskripsi produk minimal 10 karakter.")]
        public string ProductDesc { get; set; } = null!;

        [Required(ErrorMessage = "Kategori wajib dipilih.")]
        public string IdCategory { get; set; } = null!;

        [Required(ErrorMessage = "Harga wajib diisi.")]
        [Range(1000, double.MaxValue, ErrorMessage = "Harga minimal 1000.")]
        public decimal Price { get; set; }

        [Range(0, 100, ErrorMessage = "Diskon harus antara 0–100%.")]
        public decimal DiscountPercent { get; set; }

        [Required(ErrorMessage = "Stok wajib diisi.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif.")]
        public int Qty { get; set; }

        public List<string>? ProductImages { get; set; } // bisa upload beberapa gambar (base64 atau path)
    }
}