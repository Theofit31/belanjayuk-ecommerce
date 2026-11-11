using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ApiService api, ILogger<IndexModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        public List<ProductItem> Products { get; set; } = new();
        public List<string> Categories { get; set; } = new() { "Semua" };
        public string? ActiveCategory { get; set; } = "Semua";
        public string? UserName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public async Task<IActionResult> OnGetAsync(string? category)
        {
            try
            {
                // Simulasi ambil user dari cookie/token
                UserName = Request.Cookies["user_name"] ?? "Andi Kusuma";

                // Ambil kategori
                var catResponse = await _api.GetAsync<List<CategoryItem>>("category");
                if (catResponse != null)
                    Categories.AddRange(catResponse.Select(c => c.CategoryName));

                // Filter kategori
                ActiveCategory = category ?? "Semua";

                string endpoint = "products";
                if (!string.IsNullOrWhiteSpace(category) && category != "Semua")
                    endpoint = $"products/category/{category}";
                else if (!string.IsNullOrWhiteSpace(SearchQuery))
                    endpoint = $"products/search?query={SearchQuery}";

                // Ambil produk
                var result = await _api.GetAsync<List<ProductItem>>(endpoint);
                if (result != null)
                    Products = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal memuat data home");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId, int qty = 1)
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                if (userId == null)
                    return RedirectToPage("/Auth/Login");

                var data = new
                {
                    IdUser = userId,
                    IdProduct = productId,
                    Qty = qty
                };
                _logger.LogInformation("Mengirim AddToCart: {@data}", data);
                var response = await _api.PostAsync("cart/add", data);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Barang ditambahkan ke keranjang!";
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Gagal menambahkan barang. {msg}";
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error add to cart");
                TempData["Error"] = "Terjadi kesalahan server.";
                return RedirectToPage();
            }
        }

        public class ProductItem
        {
            public string IdProduct { get; set; } = null!;
            public string ProductName { get; set; } = null!;
            public List<string>? Images { get; set; }       
            public decimal Price { get; set; }
            public double AvgRating { get; set; }           
            public int Sold { get; set; }
            public string SellerName { get; set; } = null!;
        }
        public class CategoryItem
        {
            public string IdCategory { get; set; } = null!;
            public string CategoryName { get; set; } = null!;
        }

    }
}
