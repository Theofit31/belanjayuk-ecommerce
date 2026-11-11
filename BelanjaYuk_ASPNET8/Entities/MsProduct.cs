using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("MsProduct")]
    public class MsProduct
    {
        [Key]
        [StringLength(36)]
        public string IdProduct { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdUserSeller { get; set; } = null!;

        [ForeignKey(nameof(IdUserSeller))]
        public MsUserSeller? Seller { get; set; }

        [Required, StringLength(200)]
        public string ProductName { get; set; } = null!;

        [StringLength(2000)]
        public string? ProductDesc { get; set; }

        [Required, StringLength(36)]
        public string IdCategory { get; set; } = null!;

        [ForeignKey(nameof(IdCategory))]
        public LtCategory? Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal DiscountPercent { get; set; } = 0;

        [Required]
        public int Qty { get; set; }

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        // Relasi: Produk punya banyak gambar
        public ICollection<TrProductImages>? Images { get; set; }

        // Produk bisa muncul di banyak transaksi detail
        public ICollection<TrBuyerTransactionDetail>? TransactionDetails { get; set; }
    }
}
