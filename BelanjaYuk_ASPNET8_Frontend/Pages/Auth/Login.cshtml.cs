using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ApiService api, ILogger<LoginModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        [BindProperty]
        public LoginForm Input { get; set; } = new();

        public string? Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Input.EmailOrPhone))
            {
                Message = "Email atau nomor HP wajib diisi.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Input.Password))
            {
                Message = "Kata sandi wajib diisi.";
                return Page();
            }

            try
            {
                var response = await _api.PostAsync("auth/login", Input);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Simpan token + data user ke cookie
                    Response.Cookies.Append("auth_token", data!.Token, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });
                    Response.Cookies.Append("user_name", data!.UserName, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });
                    Response.Cookies.Append("user_id", data!.IdUser, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });

                    TempData["Success"] = "Login berhasil!";
                    return RedirectToPage("/Home/Index");
                }

                var error = await response.Content.ReadAsStringAsync();
                Message = error.Contains("tidak terdaftar")
                    ? "Email atau nomor HP tidak terdaftar."
                    : "Kata sandi salah.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error login");
                Message = "Terjadi kesalahan koneksi ke server.";
            }

            return Page();
        }

        public class LoginForm
        {
            public string EmailOrPhone { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class LoginResponse
        {
            public string IdUser { get; set; } = null!;
            public string UserName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Token { get; set; } = null!;
        }
    }
}
