using System;

namespace BelanjaYuk.Dtos.Auth
{
    public class RegisterResponse
    {
        public string IdUser { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}