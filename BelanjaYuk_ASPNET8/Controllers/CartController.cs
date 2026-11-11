using BelanjaYuk.Dtos.Cart;
using BelanjaYuk.Entities;
using BelanjaYuk.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace BelanjaYuk_ASPNET8.Controllers
{
    [ApiController]
    [Route("api/v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idUser}")]
        public IActionResult GetCartItems(string idUser)
        {
            var user = _context.MsUsers.FirstOrDefault(u => u.IdUser == idUser && u.IsActive);
            if (user == null)
                return NotFound(new { message = "User tidak ditemukan." });
            Console.WriteLine($"[GetCartItems] IdUser={idUser}");
            var cartItems = _context.TrBuyerCarts
                .Include(c => c.Product)!.ThenInclude(p => p.Images)
                .Where(c => c.IdUser == idUser && c.IsActive)
                .ToList();

            if (cartItems.Count == 0)
                return Ok(new { message = "Keranjang kosong.", data = new List<object>() });

            var data = cartItems.Select(c =>
            {
                var product = c.Product!;
                var discountAmount = (product.Price * product.DiscountPercent / 100);
                var finalPrice = product.Price - discountAmount;
                var subtotal = finalPrice * c.Qty;

                return new
                {
                    c.IdBuyerCart,
                    product.IdProduct,
                    product.ProductName,
                    ImageUrl = product.Images!.Any()
                        ? product.Images.First().ProductImage
                        : "/images/no-image.png",
                    PriceNormal = product.Price,
                    DiscountPercent = product.DiscountPercent,
                    DiscountDisplay = discountAmount > 0 ? $"-Rp {discountAmount:N0}" : null,
                    FinalPrice = finalPrice,
                    c.Qty,
                    Subtotal = subtotal
                };
            }).ToList();

            var totalHarga = data.Sum(x => x.Subtotal);
            var ongkir = 0; // Gratis ongkir default

            return Ok(new
            {
                message = "Data keranjang berhasil diambil.",
                totalItem = data.Count,
                biayaPengiriman = ongkir,
                totalKeseluruhan = totalHarga + ongkir,
                data
            });
        }
        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.MsUsers.FirstOrDefault(u => u.IdUser == request.IdUser && u.IsActive);
            if (user == null)
                return NotFound(new { message = "User tidak ditemukan." });

            var product = _context.MsProducts.FirstOrDefault(p => p.IdProduct == request.IdProduct && p.IsActive);
            if (product == null)
                return NotFound(new { message = "Produk tidak ditemukan." });

            if (request.Qty < 1 || request.Qty > 99)
                return BadRequest(new { message = "Kuantitas minimal 1 dan maksimal 99." });

            var existingCart = _context.TrBuyerCarts
                .FirstOrDefault(c => c.IdUser == request.IdUser && c.IdProduct == request.IdProduct);

            // aktifkan kembali item lama jika pernah dihapus
            if (existingCart != null)
            {
                existingCart.Qty += request.Qty;
                if (existingCart.Qty > 99)
                    existingCart.Qty = 99;

                // jika item sebelumnya pernah dihapus, aktifkan kembali
                existingCart.IsActive = true;

                existingCart.DateUp = DateTime.Now;
                _context.SaveChanges();

                return Ok(new { message = "Kuantitas produk di keranjang berhasil diperbarui." });
            }


            // Jika belum pernah ada, buat baru
            var newCart = new TrBuyerCart
            {
                IdBuyerCart = Guid.NewGuid().ToString(),
                IdUser = request.IdUser,
                IdProduct = request.IdProduct,
                Qty = request.Qty,
                DateIn = DateTime.Now,
                IsActive = true
            };

            _context.TrBuyerCarts.Add(newCart);
            _context.SaveChanges();
            Console.WriteLine($"[AddToCart] existingCart={existingCart != null}, IsActive={existingCart?.IsActive}");
            return Ok(new { message = "Produk berhasil ditambahkan ke keranjang." });
        }

        [HttpPut("update/{idBuyerCart}")]
        public IActionResult UpdateCartQty(string idBuyerCart, [FromBody] UpdateCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = _context.TrBuyerCarts
                .Include(c => c.Product)
                .FirstOrDefault(c => c.IdBuyerCart == idBuyerCart && c.IsActive);

            if (cart == null)
                return NotFound(new { message = "Item keranjang tidak ditemukan." });

            if (request.Qty < 1 || request.Qty > 99)
                return BadRequest(new { message = "Kuantitas harus antara 1 dan 99." });

            cart.Qty = request.Qty;
            cart.DateUp = DateTime.Now;
            _context.SaveChanges();

            // Hitung ulang subtotal
            var product = cart.Product!;
            var discountAmount = (product.Price * product.DiscountPercent / 100);
            var finalPrice = product.Price - discountAmount;
            var subtotal = finalPrice * cart.Qty;

            return Ok(new
            {
                message = "Kuantitas berhasil diperbarui.",
                cart.IdBuyerCart,
                cart.Qty,
                FinalPrice = finalPrice,
                Subtotal = subtotal
            });
        }
        [HttpDelete("{idBuyerCart}")]
        public IActionResult RemoveFromCart(string idBuyerCart)
        {
            var cart = _context.TrBuyerCarts.FirstOrDefault(c => c.IdBuyerCart == idBuyerCart && c.IsActive);
            if (cart == null)
                return NotFound(new { message = "Item keranjang tidak ditemukan." });

            cart.IsActive = false;
            cart.DateUp = DateTime.Now;
            _context.SaveChanges();

            return Ok(new { message = "Produk berhasil dihapus dari keranjang." });
        }
    }
}
