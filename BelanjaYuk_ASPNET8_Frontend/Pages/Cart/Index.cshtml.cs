using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Cart
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

        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalKeseluruhan { get; set; }
        public List<PaymentItem> Payments { get; set; } = new();

        [BindProperty]
        public string? SelectedPayment { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = Request.Cookies["user_id"];
            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Auth/Login");

            var cartResponse = await _api.GetAsync<CartResponse>($"cart/{userId}");
            if (cartResponse?.data != null)
            {
                CartItems = cartResponse.data;
                TotalKeseluruhan = cartResponse.totalKeseluruhan;
            }

            var payments = await _api.GetAsync<List<PaymentItem>>("transaction/payments");
            if (payments != null)
                Payments = payments;

            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var userId = Request.Cookies["user_id"];
            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Auth/Login");

            if (string.IsNullOrEmpty(SelectedPayment))
            {
                TempData["Error"] = "Pilih metode pembayaran terlebih dahulu.";
                return RedirectToPage();
            }

            // Ambil ulang keranjang untuk dikirim ke checkout API
            var cartResponse = await _api.GetAsync<CartResponse>($"cart/{userId}");
            if (cartResponse?.data == null || cartResponse.data.Count == 0)
            {
                TempData["Error"] = "Keranjang kosong.";
                return RedirectToPage();
            }

            var request = new
            {
                UserId = userId,
                PaymentId = SelectedPayment,
                Items = cartResponse.data.Select(i => new
                {
                    ProductId = i.IdProduct,
                    Qty = i.Qty
                }).ToList()
            };

            var response = await _api.PostAsync("transaction/checkout", request);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Checkout berhasil! Barang siap diproses.";
            }
            else
            {
                TempData["Error"] = "Gagal melakukan checkout.";
            }

            return RedirectToPage();
        }

        public class CartItem
        {
            public string IdBuyerCart { get; set; } = null!;
            public string IdProduct { get; set; } = null!;
            public string ProductName { get; set; } = null!;
            public string ImageUrl { get; set; } = null!;
            public decimal PriceNormal { get; set; }
            public decimal DiscountPercent { get; set; }
            public string? DiscountDisplay { get; set; }
            public decimal FinalPrice { get; set; }
            public int Qty { get; set; }
            public decimal Subtotal { get; set; }
        }

        public class CartResponse
        {
            public string message { get; set; } = null!;
            public int totalItem { get; set; }
            public decimal biayaPengiriman { get; set; }
            public decimal totalKeseluruhan { get; set; }
            public List<CartItem> data { get; set; } = new();
        }

        public class PaymentItem
        {
            public string IdPayment { get; set; } = null!;
            public string PaymentName { get; set; } = null!;
        }
    }
}