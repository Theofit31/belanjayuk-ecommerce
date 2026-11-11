using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("MsUserSeller")]
    public class MsUserSeller
    {
        [Key]
        [StringLength(36)]
        public string IdUserSeller { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdUser { get; set; } = null!;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }

        [Required, StringLength(100)]
        public string SellerName { get; set; } = null!;

        [StringLength(1000)]
        public string? SellerDesc { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string SellerCode { get; set; } = null!;

        [Required, StringLength(50)]
        public string PhoneNumber { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        // Relasi: Seller punya banyak produk
        public ICollection<MsProduct>? Products { get; set; }
    }
}