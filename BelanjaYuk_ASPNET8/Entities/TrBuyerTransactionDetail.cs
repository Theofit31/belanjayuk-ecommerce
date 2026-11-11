using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("TrBuyerTransactionDetail")]
    public class TrBuyerTransactionDetail
    {
        [Key]
        [StringLength(36)]
        public string IdBuyerTransactionDetail { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdBuyerTransaction { get; set; } = null!;

        [ForeignKey(nameof(IdBuyerTransaction))]
        public TrBuyerTransaction? Transaction { get; set; }

        [Required, StringLength(36)]
        public string IdProduct { get; set; } = null!;

        [ForeignKey(nameof(IdProduct))]
        public MsProduct? Product { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceProduct { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountProduct { get; set; }

        public int? Rating { get; set; }

        [StringLength(1000)]
        public string? RatingComment { get; set; }

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
