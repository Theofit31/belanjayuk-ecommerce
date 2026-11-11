using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Seller
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
        public SellerForm Input { get; set; } = new();

        public string? Message { get; set; }

        public void OnGet()
        {
            // Bisa preload data user kalau mau
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            _logger.LogInformation("Handler OnPostCreateAsync() terpanggil");

            try
            {
                var userId = Request.Cookies["user_id"];
                if (userId == null)
                {
                    _logger.LogWarning("Tidak ada cookie user_id, redirect ke login");
                    Message = "Silakan login terlebih dahulu.";
                    return RedirectToPage("/Auth/Login");
                }

                Input.IdUser = userId;

                _logger.LogInformation("Mengirim ke API seller/create dengan data: " +
                    JsonSerializer.Serialize(Input, new JsonSerializerOptions { WriteIndented = true }));

                var response = await _api.PostAsync("seller/create", Input);

                _logger.LogInformation($"StatusCode dari API: {response.StatusCode}");

                var body = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response body: {body}");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<CreateSellerResponse>(body,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7), // cookie aktif 7 hari
                        HttpOnly = false,
                        Secure = false,
                        SameSite = SameSiteMode.Lax
                    };

                    Response.Cookies.Append("seller_name", result!.SellerName, cookieOptions);
                    Response.Cookies.Append("seller_code", result!.SellerCode, cookieOptions);


                    TempData["Success"] = "Toko berhasil dibuat!";
                    return Redirect("/Home/Index");
                }

                Message = "Gagal membuat toko. Status: " + response.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal daftar seller");
                Message = "Terjadi kesalahan server.";
            }
            return Page();
        }

        public class SellerForm
        {
            public string SellerName { get; set; } = null!;
            public string SellerCode { get; set; } = null!;
            public string? PhoneNumber { get; set; }
            public string SellerDesc { get; set; } = null!;
            public string Address { get; set; } = null!;
            public bool Agreement { get; set; }
            public string IdUser { get; set; } = null!;
        }

        public class CreateSellerResponse
        {
            public string IdUserSeller { get; set; } = null!;
            public string SellerName { get; set; } = null!;
            public string SellerCode { get; set; } = null!;
        }
    }
}
