using System;

namespace BelanjaYuk.Dtos.Auth
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public DateTime DOB { get; set; }
        public string IdGender { get; set; } = null!;  // FK ke LtGender

        // Optional alamat utama
        public string? Provinsi { get; set; }
        public string? KotaKabupaten { get; set; }
        public string? Kecamatan { get; set; }
        public string? KodePos { get; set; }
        public string? HomeAddressDesc { get; set; }
    }
}