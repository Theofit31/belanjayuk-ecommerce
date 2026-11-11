using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("TrProductImages")]
    public class TrProductImages
    {
        [Key]
        [StringLength(36)]
        public string IdProductImages { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(36)]
        public string IdProduct { get; set; } = null!;

        [ForeignKey(nameof(IdProduct))]
        public MsProduct? Product { get; set; }

        [Required]
        public string ProductImage { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; } 
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;
    }
}