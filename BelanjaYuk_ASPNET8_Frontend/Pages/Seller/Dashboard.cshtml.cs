using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Seller
{
    public class DashboardModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<DashboardModel> _logger;

        public DashboardModel(ApiService api, ILogger<DashboardModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        public string SellerName { get; set; } = "-";
        public string SellerId { get; set; } = "-";
        public int ActiveProducts { get; set; }
        public int LowStock { get; set; }
        public int OrderThisMonth { get; set; }
        public string TopProduct { get; set; } = "-";
        public string ProfitThisMonth { get; set; } = "-";

        public string SellerInitial =>
            string.Join("", SellerName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(s => s[0]))
            .ToUpper();

        public async Task OnGetAsync()
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User belum login");
                    return;
                }

                var endpoint = $"seller/dashboard/{userId}";
                var result = await _api.GetAsync<SellerDashboardResponse>(endpoint);

                if (result != null)
                {
                    SellerName = result.SellerName;
                    SellerId = result.SellerId;
                    ActiveProducts = result.ProductActive;
                    LowStock = result.ProductLowStock;
                    OrderThisMonth = result.OrderThisMonth;
                    TopProduct = result.TopProduct;
                    ProfitThisMonth = $"Rp {result.ProfitThisMonth:N0}";
                }
                else
                {
                    _logger.LogWarning("Tidak ada data dashboard dari API");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal memuat dashboard seller");
            }
        }

        public class SellerDashboardResponse
        {
            public string SellerName { get; set; } = "-";
            public string SellerId { get; set; } = "-";
            public int ProductActive { get; set; }
            public int ProductLowStock { get; set; }
            public int OrderThisMonth { get; set; }
            public string TopProduct { get; set; } = "-";
            public decimal ProfitThisMonth { get; set; }
        }
    }
}