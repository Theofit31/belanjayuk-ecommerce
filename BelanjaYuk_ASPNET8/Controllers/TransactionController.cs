using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelanjaYuk.Infrastructure;
using BelanjaYuk.Entities;
using BelanjaYuk.Dtos.Transaction;

namespace BelanjaYuk.Controllers
{
    [ApiController]
    [Route("api/v1/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("payments")]
        public IActionResult GetPayments()
        {
            var payments = _context.LtPayments
                .Where(p => p.IsActive)
                .Select(p => new { p.IdPayment, p.PaymentName })
                .ToList();

            return Ok(payments);
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CheckoutRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validasi user
            var user = _context.MsUsers.FirstOrDefault(u => u.IdUser == request.UserId && u.IsActive);
            if (user == null)
                return NotFound(new { message = "User tidak ditemukan." });

            // Validasi metode pembayaran
            var payment = _context.LtPayments.FirstOrDefault(p => p.IdPayment == request.PaymentId && p.IsActive);
            if (payment == null)
                return BadRequest(new { message = "Metode pembayaran tidak valid." });

            // Validasi item
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Daftar item tidak boleh kosong." });

            decimal totalCalculated = 0;

            foreach (var item in request.Items)
            {
                var product = _context.MsProducts.FirstOrDefault(p => p.IdProduct == item.ProductId && p.IsActive);
                if (product == null)
                    return NotFound(new { message = $"Produk dengan ID {item.ProductId} tidak ditemukan." });

                var discountAmount = product.Price * product.DiscountPercent / 100;
                var finalPrice = product.Price - discountAmount;
                totalCalculated += finalPrice * item.Qty;

                if (item.Qty < 1)
                    return BadRequest(new { message = "Jumlah produk minimal 1." });
            }

            request.TotalPrice ??= totalCalculated; // isi hanya kalau belum dikirim

            // Simpan transaksi utama
            var transaction = new TrBuyerTransaction
            {
                IdBuyerTransaction = Guid.NewGuid().ToString(),
                IdUser = request.UserId,
                IdPayment = request.PaymentId,
                FinalPrice = request.TotalPrice ?? totalCalculated,
                DateIn = DateTime.Now,
                IsActive = true
            };

            _context.TrBuyerTransactions.Add(transaction);
            _context.SaveChanges();

            // Simpan detail transaksi
            foreach (var item in request.Items)
            {
                var product = _context.MsProducts.First(p => p.IdProduct == item.ProductId);

                var discountAmount = product.Price * product.DiscountPercent / 100;
                var finalPrice = product.Price - discountAmount;

                var detail = new TrBuyerTransactionDetail
                {
                    IdBuyerTransactionDetail = Guid.NewGuid().ToString(),
                    IdBuyerTransaction = transaction.IdBuyerTransaction,
                    IdProduct = product.IdProduct,
                    Qty = item.Qty,
                    PriceProduct = product.Price,
                    DiscountProduct = discountAmount,
                    DateIn = DateTime.Now,
                    IsActive = true
                };

                _context.TrBuyerTransactionDetails.Add(detail);
            }

            _context.SaveChanges();

            // Kosongkan keranjang user setelah checkout
            var carts = _context.TrBuyerCarts
                .Where(c => c.IdUser == request.UserId && c.IsActive)
                .ToList();

            foreach (var cart in carts)
            {
                cart.IsActive = false;
                cart.DateUp = DateTime.Now;
            }

            _context.SaveChanges();

            return Ok(new
            {
                message = "Checkout berhasil.",
                transactionId = transaction.IdBuyerTransaction,
                totalHarga = request.TotalPrice,
                metodePembayaran = payment.PaymentName,
                jumlahItem = request.Items.Count
            });
        }
    }
}