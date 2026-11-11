using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("TrBuyerTransaction")]
    public class TrBuyerTransaction
    {
        [Key]
        [StringLength(36)]
        public string IdBuyerTransaction { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdUser { get; set; } = null!;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }

        [Required, StringLength(36)]
        public string IdPayment { get; set; } = null!;

        [ForeignKey(nameof(IdPayment))]
        public LtPayment? Payment { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalPrice { get; set; }

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

        // Relasi: transaksi punya banyak detail
        public ICollection<TrBuyerTransactionDetail>? Details { get; set; }
    }
}