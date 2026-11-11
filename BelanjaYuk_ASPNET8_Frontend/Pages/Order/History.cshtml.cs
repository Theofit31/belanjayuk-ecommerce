using BelanjaYuk_ASPNET8_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Pages.Order
{
    public class HistoryModel : PageModel
    {
        private readonly ApiService _api;
        private readonly ILogger<HistoryModel> _logger;

        public HistoryModel(ApiService api, ILogger<HistoryModel> logger)
        {
            _api = api;
            _logger = logger;
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public List<OrderItem> Orders { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(DateTime? fromDate, DateTime? toDate)
        {
            var idUser = Request.Cookies["user_id"];
            if (string.IsNullOrEmpty(idUser))
                return RedirectToPage("/Auth/Login");

            FromDate = fromDate;
            ToDate = toDate;

            string query = $"orders/{idUser}";
            if (fromDate.HasValue && toDate.HasValue)
                query += $"?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}";

            var res = await _api.GetAsync<OrderResponse>(query);
            if (res?.data != null)
                Orders = res.data;

            return Page();
        }

        public class OrderResponse
        {
            public int total { get; set; }
            public List<OrderItem> data { get; set; } = new();
        }

        public class OrderItem
        {
            public string Invoice => IdBuyerTransaction.Substring(0, 12);
            public string IdBuyerTransaction { get; set; } = null!;
            public DateTime DateIn { get; set; }   
            public decimal FinalPrice { get; set; } 
            public List<ProductItem> Products { get; set; } = new();
        }

        public class ProductItem
        {
            public string IdDetail { get; set; } = null!;
            public string IdProduct { get; set; } = null!;
            public string ProductName { get; set; } = null!;
            public string ProductImage { get; set; } = null!;
            public int Qty { get; set; }
            public decimal PriceProduct { get; set; }
            public decimal Subtotal { get; set; }
            public int? Rating { get; set; }
            public string? Comment { get; set; }
            public decimal Total { get; set; }
        }

    }
}