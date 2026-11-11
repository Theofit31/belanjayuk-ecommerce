namespace BelanjaYuk.Dtos.Auth
{
    public class LoginRequest
    {
        public string EmailOrPhone { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}