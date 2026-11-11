using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("LtCategory")]
    public class LtCategory
    {
        [Key]
        [StringLength(36)]
        public string IdCategory { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(100)]
        public string CategoryName { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;
        [StringLength(36)]
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        [StringLength(36)]
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        // Relasi: Kategori punya banyak produk
        public ICollection<MsProduct>? Products { get; set; }
    }
}