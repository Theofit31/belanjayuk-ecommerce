using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelanjaYuk.Infrastructure;
using BelanjaYuk.Entities;
using BelanjaYuk.Dtos.Seller;

namespace BelanjaYuk.Controllers
{
    [ApiController]
    [Route("api/v1/seller")]
    public class SellerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SellerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateSeller([FromBody] CreateSellerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.MsUsers.FirstOrDefault(u => u.IdUser == request.IdUser);
            if (user == null)
                return NotFound(new { message = "User tidak ditemukan." });

            // Cek URL toko
            if (_context.MsUserSellers.Any(s => s.SellerCode.ToLower() == request.SellerCode.ToLower()))
                return Conflict(new { message = "URL toko sudah digunakan." });

            // Generate otomatis SellerCode jika kosong
            if (string.IsNullOrWhiteSpace(request.SellerCode))
                request.SellerCode = request.SellerName.Trim().ToLower().Replace(" ", "-");

            var newSeller = new MsUserSeller
            {
                IdUserSeller = Guid.NewGuid().ToString(),
                IdUser = request.IdUser,
                SellerName = request.SellerName.Trim(),
                SellerDesc = request.SellerDesc.Trim(),
                Address = request.Address.Trim(),
                SellerCode = request.SellerCode.Trim().ToLower(),
                PhoneNumber = request.PhoneNumber ?? "",
                IsActive = true,
                DateIn = DateTime.Now
            };

            _context.MsUserSellers.Add(newSeller);
            _context.SaveChanges();

            var response = new CreateSellerResponse
            {
                IdUserSeller = newSeller.IdUserSeller,
                SellerName = newSeller.SellerName,
                SellerCode = newSeller.SellerCode
            };

            return Ok(response);
        }

        [HttpGet("dashboard/{id}")]
        public IActionResult GetSellerDashboard(string id)
        {
            // Cari seller berdasarkan IdUser atau IdUserSeller
            var seller = _context.MsUserSellers
                .AsEnumerable()
                .FirstOrDefault(s =>
                    s.IdUser?.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase) == true ||
                    s.IdUserSeller?.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase) == true);

            if (seller == null)
                return NotFound(new { message = $"Seller tidak ditemukan untuk ID: {id}" });

            // Ambil semua produk milik seller ini
            var products = _context.MsProducts
                .Where(p => p.IdUserSeller == seller.IdUserSeller)
                .ToList();

            var productActive = products.Count(p => p.IsActive);
            var productLowStock = products.Count(p => p.Qty < 5); 

            // Ambil transaksi bulan ini
            var startMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var transactions = _context.TrBuyerTransactions
                .Include(t => t.Details!)
                    .ThenInclude(d => d.Product)
                .Where(t => t.DateIn >= startMonth &&
                            t.Details!.Any(d => d.Product!.IdUserSeller == seller.IdUserSeller))
                .ToList();

            var orderCount = transactions.Count;

            // Total profit dari semua transaksi bulan ini
            var totalProfit = transactions.Sum(t =>
                t.Details!.Sum(d => d.PriceProduct * d.Qty)
            );

            // Produk terlaris
            var topProduct = transactions
                .SelectMany(t => t.Details!)
                .Where(d => d.Product!.IdUserSeller == seller.IdUserSeller)
                .GroupBy(d => d.Product!.ProductName)
                .Select(g => new { ProductName = g.Key, TotalQty = g.Sum(x => x.Qty) })
                .OrderByDescending(g => g.TotalQty)
                .FirstOrDefault();

            // Response untuk frontend
            var response = new
            {
                SellerName = seller.SellerName,
                SellerId = seller.IdUserSeller,
                ProductActive = productActive,
                ProductLowStock = productLowStock,
                OrderThisMonth = orderCount,
                TopProduct = topProduct?.ProductName ?? "-",
                ProfitThisMonth = totalProfit,
                DateJoined = seller.DateIn.ToString("dd MMMM yyyy")
            };

            return Ok(response);
        }
    }
}
