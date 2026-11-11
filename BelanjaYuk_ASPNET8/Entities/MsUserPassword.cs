using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("MsUserPassword")]
    public class MsUserPassword
    {
        [Key]
        [StringLength(36)]
        public string IdUserPassword { get; set; } = Guid.NewGuid().ToString();
        [Required, StringLength(36)]
        public string IdUser { get; set; } = null!;

        [StringLength(200)]
        public string PasswordHash { get; set; } = null!;

        public DateTime DateIn { get; set; } = DateTime.Now;
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }
    }
}