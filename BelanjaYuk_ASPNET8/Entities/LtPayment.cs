using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("LtPayment")]
    public class LtPayment
    {
        [Key]
        [StringLength(36)]
        public string IdPayment { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(100)]
        public string PaymentName { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        // Relasi: satu metode pembayaran bisa digunakan banyak transaksi
        public ICollection<TrBuyerTransaction>? Transactions { get; set; }
    }
}