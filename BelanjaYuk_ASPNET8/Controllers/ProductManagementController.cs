using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelanjaYuk.Infrastructure;
using BelanjaYuk.Entities;
using BelanjaYuk.Dtos.Product;

namespace BelanjaYuk.Controllers
{
    [ApiController]
    [Route("api/v1/seller/{idSeller}/products")]
    public class ProductManagementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductManagementController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetSellerProducts(string idSeller, [FromQuery] string? search)
        {
            // Validasi seller
            var seller = _context.MsUserSellers
                .Include(s => s.Products)
                .FirstOrDefault(s => s.IdUserSeller == idSeller && s.IsActive);

            if (seller == null)
                return NotFound(new { message = "Seller tidak ditemukan." });

            var query = _context.MsProducts
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.IdUserSeller == idSeller && p.IsActive);

            // Jika ada parameter pencarian
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                if (search.Length < 2 || !search.Any(char.IsLetterOrDigit))
                    return BadRequest(new { message = "Pencarian minimal 2 karakter dan tidak boleh hanya simbol." });

                query = query.Where(p => p.ProductName.ToLower().Contains(search));
            }

            var products = query
                .OrderByDescending(p => p.DateIn)
                .Select(p => new
                {
                    p.IdProduct,
                    p.ProductName,
                    p.ProductDesc,
                    p.Price,
                    p.DiscountPercent,
                    FinalPrice = p.Price - (p.Price * p.DiscountPercent / 100),
                    p.Qty,
                    Category = p.Category!.CategoryName,
                    ImageUrl = (p.Images != null && p.Images.Any() && !string.IsNullOrEmpty(p.Images.First().ProductImage))
                        ? p.Images.First().ProductImage
                        : "http://localhost:5283/img/no-image.jpg",
                    p.IsActive
                })
                .ToList();

            if (products.Count == 0)
                return Ok(new { message = "Belum ada produk.", data = new List<object>() });

            return Ok(new { total = products.Count, data = products });
        }

        
        [HttpPost]
        public IActionResult CreateProduct(string idSeller, [FromBody] CreateProductRequest request)
        {
            Console.WriteLine($"[API] CreateProduct SellerId={idSeller}, Product={request.ProductName}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validasi seller
            var seller = _context.MsUserSellers.FirstOrDefault(s => s.IdUserSeller == idSeller && s.IsActive);
            if (seller == null)
                return NotFound(new { message = "Seller tidak ditemukan." });

            // Validasi kategori
            var category = _context.LtCategories.FirstOrDefault(c => c.IdCategory == request.IdCategory && c.IsActive);
            if (category == null)
                return NotFound(new { message = "Kategori tidak ditemukan." });

            // Buat produk baru
            var product = new MsProduct
            {
                IdProduct = Guid.NewGuid().ToString(),
                IdUserSeller = idSeller,
                ProductName = request.ProductName.Trim(),
                ProductDesc = request.ProductDesc.Trim(),
                IdCategory = request.IdCategory,
                Price = request.Price,
                DiscountPercent = request.DiscountPercent,
                Qty = request.Qty,
                DateIn = DateTime.Now,
                IsActive = true
            };

            _context.MsProducts.Add(product);
            _context.SaveChanges();

            // Simpan gambar produk
            if (request.ProductImages != null && request.ProductImages.Count > 0)
            {
                foreach (var img in request.ProductImages)
                {
                    string formattedImg = img.StartsWith("data:image")
                        ? img
                        : $"data:image/jpeg;base64,{img}";

                    var image = new TrProductImages
                    {
                        IdProductImages = Guid.NewGuid().ToString(),
                        IdProduct = product.IdProduct,
                        ProductImage = formattedImg,
                        DateIn = DateTime.Now,
                        IsActive = true
                    };
                    _context.TrProductImages.Add(image);
                }
            }
            else
            {
                // Jika seller tidak upload gambar
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                _context.TrProductImages.Add(new TrProductImages
                {
                    IdProductImages = Guid.NewGuid().ToString(),
                    IdProduct = product.IdProduct,
                    ProductImage = $"{baseUrl}/img/no-image.jpg", 
                    DateIn = DateTime.Now,
                    IsActive = true
                });
            }

            _context.SaveChanges();

            return Ok(new
            {
                message = "Produk berhasil ditambahkan.",
                product.IdProduct,
                product.ProductName
            });
        }

        // Update data produk
        [HttpPut("{idProduct}")]
        public IActionResult UpdateProduct(string idSeller, string idProduct, [FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _context.MsProducts
                .Include(p => p.Images)
                .FirstOrDefault(p => p.IdProduct == idProduct && p.IdUserSeller == idSeller && p.IsActive);

            if (product == null)
                return NotFound(new { message = "Produk tidak ditemukan." });

            // Update data
            product.ProductName = request.ProductName.Trim();
            product.ProductDesc = request.ProductDesc.Trim();
            product.Price = request.Price;
            product.DiscountPercent = request.DiscountPercent;
            product.Qty = request.Qty;
            product.IdCategory = request.IdCategory;
            product.DateUp = DateTime.Now;

            _context.MsProducts.Update(product);

            // Update gambar
            if (request.ProductImages != null && request.ProductImages.Count > 0)
            {
                var oldImages = _context.TrProductImages.Where(i => i.IdProduct == idProduct).ToList();
                _context.TrProductImages.RemoveRange(oldImages);

                foreach (var img in request.ProductImages)
                {
                    var formattedImage = img.StartsWith("data:image")
                        ? img
                        : $"data:image/jpeg;base64,{img}";

                    _context.TrProductImages.Add(new TrProductImages
                    {
                        IdProductImages = Guid.NewGuid().ToString(),
                        IdProduct = product.IdProduct,
                        ProductImage = formattedImage,
                        DateIn = DateTime.Now,
                        IsActive = true
                    });
                }
            }

            _context.SaveChanges();

            return Ok(new { message = "Produk berhasil diperbarui." });
        }

        // Dapatkan detail produk
        [HttpGet("{idProduct}")]
        public IActionResult GetProductById(string idSeller, string idProduct)
        {
            var product = _context.MsProducts
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefault(p => p.IdUserSeller == idSeller && p.IdProduct == idProduct && p.IsActive);

            if (product == null)
                return NotFound(new { message = "Produk tidak ditemukan." });

            return Ok(new
            {
                product.IdProduct,
                product.ProductName,
                product.ProductDesc,
                product.Price,
                product.DiscountPercent,
                product.Qty,
                product.IdCategory,
                product.Category!.CategoryName,
                ImageUrls = product.Images!.Select(i => i.ProductImage).ToList()
            });
        }

        // Soft delete produk (nonaktifkan)
        [HttpDelete("{idProduct}")]
        public IActionResult SoftDeleteProduct(string idSeller, string idProduct)
        {
            var product = _context.MsProducts
                .FirstOrDefault(p => p.IdProduct == idProduct && p.IdUserSeller == idSeller);

            if (product == null)
                return NotFound(new { message = "Produk tidak ditemukan." });

            product.IsActive = false;
            product.DateUp = DateTime.Now;

            _context.SaveChanges();

            return Ok(new { message = "Produk berhasil di-nonaktifkan (soft delete)." });
        }
    }
}