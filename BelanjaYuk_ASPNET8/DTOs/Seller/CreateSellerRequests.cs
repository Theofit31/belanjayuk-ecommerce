using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.Dtos.Seller
{
    public class CreateSellerRequest
    {
        [Required(ErrorMessage = "Nama toko wajib diisi.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-]{3,40}$",
            ErrorMessage = "Nama toko hanya boleh huruf, angka, spasi, dan tanda hubung (-), 3–40 karakter.")]
        public string SellerName { get; set; } = null!;

        // Dibentuk otomatis dari nama toko (bisa diubah)
        [Required(ErrorMessage = "URL toko wajib diisi.")]
        [RegularExpression(@"^[a-z0-9\-]{3,40}$",
            ErrorMessage = "URL toko hanya boleh huruf kecil, angka, dan tanda hubung (-).")]
        public string SellerCode { get; set; } = null!;

        [RegularExpression(@"^(\+628|08)\d{8,11}$",
            ErrorMessage = "Nomor HP tidak valid. Gunakan format Indonesia (08 atau +628) dengan panjang 10–13 digit.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Deskripsi toko wajib diisi.")]
        [StringLength(2000, MinimumLength = 30,
            ErrorMessage = "Deskripsi harus antara 30 hingga 2000 karakter.")]
        public string SellerDesc { get; set; } = null!;

        [Required(ErrorMessage = "Alamat toko wajib diisi.")]
        [MinLength(10, ErrorMessage = "Alamat minimal 10 karakter.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Persetujuan wajib dicentang.")]
        public bool Agreement { get; set; }

        [Required]
        public string IdUser { get; set; } = null!;
    }
}
