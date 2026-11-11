using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelanjaYuk.Entities
{
    [Table("TrHomeAddress")]
    public class TrHomeAddress
    {
        [Key]
        [StringLength(36)]
        public string IdHomeAddress { get; set; } = Guid.NewGuid().ToString();

        public string IdUser { get; set; } = null!;

        [Required, StringLength(200)]
        public string? Provinsi { get; set; }

        [Required, StringLength(200)]
        public string? KotaKabupaten { get; set; }

        [Required, StringLength(200)]
        public string? Kecamatan { get; set; }

        [Required, StringLength(10)]
        public string? KodePos { get; set; }

        [StringLength(2000)]
        public string? HomeAddressDesc { get; set; }

        public bool IsPrimaryAddress { get; set; } = false;

        public DateTime DateIn { get; set; } = DateTime.Now;
        public string? UserIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserUp { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }
    }
}
