using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("LtGender")]
    public class LtGender
    {
        [Key]
        [StringLength(36)]
        public string IdGender { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(50)]
        public string GenderName { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;

        [StringLength(36)]
        public string? UserIn { get; set; }

        public DateTime? DateUp { get; set; }

        [StringLength(36)]
        public string? UserUp { get; set; }

        public bool IsActive { get; set; } = true;

        // Relasi: 1 Gender bisa punya banyak User
        public ICollection<MsUser>? Users { get; set; }
    }
}
