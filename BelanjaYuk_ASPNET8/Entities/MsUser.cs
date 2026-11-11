using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("MsUser")]
    public class MsUser
    {
        [Key]
        [StringLength(36)]
        public string IdUser { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(100)]
        public string UserName { get; set; } = null!;

        [Required, StringLength(100)]
        public string Email { get; set; } = null!;

        [Required, StringLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [Required, StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(200)]
        public string? LastName { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DateIn { get; set; } = DateTime.Now;

        [StringLength(36)]
        public string? UserIn { get; set; }

        public DateTime? DateUp { get; set; }

        [StringLength(36)]
        public string? UserUp { get; set; }

        public bool IsActive { get; set; } = true;

        // Relasi ke tabel LtGender
        [StringLength(36)]
        public string? IdGender { get; set; }

        [ForeignKey(nameof(IdGender))]
        public LtGender? Gender { get; set; }

        // Relasi: 1 user bisa punya banyak data lain
        public ICollection<TrBuyerCart>? Carts { get; set; }
        public ICollection<MsUserPassword>? Passwords { get; set; }
        public ICollection<MsUserSeller>? Sellers { get; set; }
        public ICollection<TrHomeAddress>? HomeAddresses { get; set; }
        public ICollection<TrBuyerTransaction>? Transactions { get; set; }
    }
}
