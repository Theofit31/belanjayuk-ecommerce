using System.Net.Http.Json;
using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(ApiService api, ILogger<RegisterModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        [BindProperty]
        public RegisterForm Input { get; set; } = new();

        public string? Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var response = await _api.PostAsync("auth/register", Input);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                    TempData["Success"] = result?.Message ?? "Registrasi berhasil!";
                    return RedirectToPage("/Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Message = error;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error register");
                Message = "Terjadi kesalahan koneksi ke server.";
            }

            return Page();
        }
    }

    public class RegisterForm
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public DateTime DOB { get; set; }
        public string IdGender { get; set; } = null!;
        public string? Provinsi { get; set; }
        public string? KotaKabupaten { get; set; }
        public string? Kecamatan { get; set; }
        public string? KodePos { get; set; }
        public string? HomeAddressDesc { get; set; }
    }

    public class RegisterResponse
    {
        public string IdUser { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
