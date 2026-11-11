using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Seller
{
    [BindProperties]
    public class ProductsModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<ProductsModel> _logger;

        public ProductsModel(ApiService api, ILogger<ProductsModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        public string SellerName { get; set; } = "-";
        [BindProperty(SupportsGet = true)]
        public string SellerId { get; set; } = "-";
        public string JoinedDate { get; set; } = "-";
        public List<ProductData> Products { get; set; } = new();
        public List<CategoryData> Categories { get; set; } = new();

        [BindProperty]
        public CreateOrUpdateProductRequest NewProduct { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                var sellerData = await _api.GetAsync<JsonElement>($"seller/dashboard/{userId}");

                SellerId = sellerData.GetProperty("sellerId").GetString()!;
                SellerName = sellerData.GetProperty("sellerName").GetString()!;
                JoinedDate = sellerData.GetProperty("dateJoined").GetString()!;

                var prodResp = await _api.GetAsync<JsonElement>($"seller/{SellerId}/products");
                if (prodResp.TryGetProperty("data", out var prodData))
                {
                    Products = prodData.EnumerateArray()
                        .Select(p => new ProductData
                        {
                            IdProduct = p.GetProperty("idProduct").GetString()!,
                            ProductName = p.GetProperty("productName").GetString()!,
                            Price = p.GetProperty("finalPrice").GetDecimal(),
                            Qty = p.GetProperty("qty").GetInt32(),
                            ImageUrl = p.GetProperty("imageUrl").GetString()!
                        }).ToList();
                }

                Categories = await _api.GetAsync<List<CategoryData>>("category");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal memuat data produk seller");
            }
        }

        public async Task<IActionResult> OnPostSaveProductAsync()
        {
            try
            {
                // Ambil ID toko dari form atau cookie fallback
                var sellerId = SellerId;
                if (string.IsNullOrEmpty(sellerId) || sellerId == "-")
                    sellerId = Request.Cookies["seller_id"];

                if (string.IsNullOrEmpty(sellerId))
                {
                    _logger.LogError("Seller ID tidak ditemukan.");
                    TempData["Error"] = "Seller ID tidak ditemukan. Silakan reload halaman.";
                    return RedirectToPage("/Seller/Products");
                }

                var imageList = new List<string>();
                if (NewProduct.ProductImages != null && NewProduct.ProductImages.Count > 0)
                {
                    foreach (var file in NewProduct.ProductImages)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        imageList.Add($"data:{file.ContentType};base64,{base64}");
                    }
                }

                var payload = new
                {
                    NewProduct.ProductName,
                    NewProduct.ProductDesc,
                    NewProduct.IdCategory,
                    NewProduct.Price,
                    NewProduct.DiscountPercent,
                    NewProduct.Qty,
                    ProductImages = imageList
                };

                // Cek mode tambah / edit
                if (string.IsNullOrEmpty(NewProduct.IdProduct))
                {
                    var res = await _api.PostAsync($"seller/{sellerId}/products", payload);
                    _logger.LogInformation("Response POST: {res}", res);
                    TempData["Success"] = "Produk berhasil ditambahkan.";
                }
                else
                {
                    await _api.PutAsync($"seller/{sellerId}/products/{NewProduct.IdProduct}", payload);
                    TempData["Success"] = "Produk berhasil diperbarui.";
                }

                return RedirectToPage("/Seller/Products");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal menyimpan produk");
                TempData["Error"] = "Terjadi kesalahan saat menyimpan produk.";
                return RedirectToPage("/Seller/Products");
            }
        }
        public async Task<IActionResult> OnPostDeleteProductAsync(string idProduct)
        {
            try
            {
                var sellerId = SellerId;
                if (string.IsNullOrEmpty(sellerId) || sellerId == "-")
                    sellerId = Request.Cookies["seller_id"];

                if (string.IsNullOrEmpty(sellerId))
                {
                    TempData["Error"] = "Seller ID tidak ditemukan.";
                    return RedirectToPage("/Seller/Products");
                }

                // panggil API soft delete
                var response = await _api.DeleteAsync($"seller/{sellerId}/products/{idProduct}");

                if (!response.IsSuccessStatusCode)
                {
                    var errMsg = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Gagal menghapus produk: {msg}", errMsg);
                    TempData["Error"] = "Gagal menghapus produk.";
                }
                else
                {
                    TempData["Success"] = "Produk berhasil dihapus.";
                }

                return RedirectToPage("/Seller/Products");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Terjadi kesalahan saat menghapus produk");
                TempData["Error"] = "Terjadi kesalahan saat menghapus produk.";
                return RedirectToPage("/Seller/Products");
            }
        }
    }

    public class ProductData
    {
        public string IdProduct { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image.png";
    }

    public class CategoryData
    {
        public string IdCategory { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }

    public class CreateOrUpdateProductRequest
    {
        public string? IdProduct { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductDesc { get; set; } = "";
        public string IdCategory { get; set; } = "";
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int Qty { get; set; }

        // untuk unggah gambar
        public List<IFormFile>? ProductImages { get; set; }
    }
}