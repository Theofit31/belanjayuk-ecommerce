using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("TrBuyerCart")]
    public class TrBuyerCart
    {
        [Key]
        [StringLength(36)]
        public string IdBuyerCart { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdUser { get; set; } = null!;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }

        [Required, StringLength(36)]
        public string IdProduct { get; set; } = null!;

        [ForeignKey(nameof(IdProduct))]
        public MsProduct? Product { get; set; }

        [Required]
        public int Qty { get; set; }

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
