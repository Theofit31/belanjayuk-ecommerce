using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelanjaYuk.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace BelanjaYuk.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET Semua Produk untuk Beranda
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.MsProducts
                .Where(p => p.IsActive)
                .Include(p => p.Images)
                .Include(p => p.Seller)
                .GroupJoin(
                    _context.TrBuyerTransactionDetails,
                    p => p.IdProduct,
                    t => t.IdProduct,
                    (p, details) => new
                    {
                        p.IdProduct,
                        p.ProductName,
                        p.Price,
                        Images = p.Images.Select(i => i.ProductImage).ToList(),
                        SellerName = p.Seller.SellerName,
                        AvgRating = details.Any() ? details.Average(d => d.Rating ?? 0) : 0,
                        Sold = details.Sum(d => d.Qty)
                    }
                )
                .ToListAsync();

            return Ok(products);
        }

        // GET Produk Berdasarkan Kategori
        [HttpGet("category/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return BadRequest(new { message = "Nama kategori tidak boleh kosong." });

            string keyword = categoryName.Trim().ToLower();

            var products = await _context.MsProducts
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.Images)
                .Where(p => p.Category!.CategoryName.ToLower() == keyword && p.IsActive)
                .GroupJoin(
                    _context.TrBuyerTransactionDetails,
                    p => p.IdProduct,
                    t => t.IdProduct,
                    (p, details) => new
                    {
                        p.IdProduct,
                        p.ProductName,
                        p.Price,
                        Images = p.Images.Select(i => i.ProductImage).ToList(),
                        SellerName = p.Seller.SellerName,
                        AvgRating = details.Any() ? details.Average(d => d.Rating ?? 0) : 0,
                        Sold = details.Sum(d => d.Qty)
                    }
                )
                .ToListAsync();

            if (products.Count == 0)
                return NotFound(new { message = "Tidak ada produk di kategori ini." });

            return Ok(products);
        }

        // GET Pencarian Produk (Search)
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Kata kunci pencarian wajib diisi." });

            var products = await _context.MsProducts
                .Where(p => p.IsActive && (p.ProductName.Contains(query) || p.ProductDesc.Contains(query)))
                .Include(p => p.Images)
                .Include(p => p.Seller)
                .GroupJoin(
                    _context.TrBuyerTransactionDetails,
                    p => p.IdProduct,
                    t => t.IdProduct,
                    (p, details) => new
                    {
                        p.IdProduct,
                        p.ProductName,
                        p.Price,
                        Images = p.Images.Select(i => i.ProductImage).ToList(),
                        SellerName = p.Seller.SellerName,
                        AvgRating = details.Any() ? details.Average(d => d.Rating ?? 0) : 0,
                        Sold = details.Sum(d => d.Qty)
                    }
                )
                .ToListAsync();

            if (products.Count == 0)
                return NotFound(new { message = "Produk tidak ditemukan." });

            return Ok(products);
        }
    }
}
