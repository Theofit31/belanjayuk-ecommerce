using BelanjaYuk.Entities;
using BelanjaYuk.Infrastructure;
using BelanjaYuk_ASPNET8.DTOs.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BelanjaYuk_ASPNET8.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idUser}")]
        public IActionResult GetUserOrders(string idUser, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var user = _context.MsUsers.FirstOrDefault(u => u.IdUser == idUser && u.IsActive);
            if (user == null)
                return NotFound(new { message = "User tidak ditemukan." });

            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                return BadRequest(new { message = "Tanggal awal tidak boleh lebih besar dari tanggal akhir." });

            var query = _context.TrBuyerTransactions
                .Include(t => t.Payment)
                .Include(t => t.Details)!.ThenInclude(d => d.Product)!.ThenInclude(p => p.Images)
                .Where(t => t.IdUser == idUser && t.IsActive)
                .AsQueryable();

            if (fromDate.HasValue && toDate.HasValue)
                query = query.Where(t => t.DateIn.Date >= fromDate.Value.Date && t.DateIn.Date <= toDate.Value.Date);

            var transactions = query
                .OrderByDescending(t => t.DateIn)
                .Select(t => new
                {
                    t.IdBuyerTransaction,
                    FinalPrice = t.FinalPrice,
                    PaymentMethod = t.Payment!.PaymentName,
                    DateIn = t.DateIn,
                    Products = t.Details!.Select(d => new
                    {
                        IdDetail = d.IdBuyerTransactionDetail,
                        d.IdProduct,
                        ProductName = d.Product!.ProductName,
                        ProductImage = d.Product.Images!.Any()
                            ? "http://localhost:5283" + d.Product.Images.First().ProductImage
                            : "http://localhost:5283/img/no-image.jpg",
                        d.Qty,
                        d.PriceProduct,
                        d.DiscountProduct,
                        Subtotal = (d.PriceProduct - (d.DiscountProduct ?? 0)) * d.Qty,
                        d.Rating,
                        d.RatingComment
                    })
                })
                .ToList();

            if (transactions.Count == 0)
                return Ok(new { message = "Belum ada riwayat pesanan.", data = new List<object>() });

            return Ok(new { total = transactions.Count, data = transactions });
        }

        [HttpPut("{transactionDetailId}/review")]
        public IActionResult UpdateReview(string transactionDetailId, [FromBody] ReviewRequest request)
        {
            if (request.Rating < 1 || request.Rating > 5)
                return BadRequest(new { message = "Rating harus antara 1 hingga 5." });

            if (!string.IsNullOrWhiteSpace(request.Comment))
            {
                if (request.Comment.Length < 5)
                    return BadRequest(new { message = "Komentar terlalu pendek." });
                if (request.Comment.Length > 1000)
                    return BadRequest(new { message = "Komentar maksimal 1000 karakter." });
            }

            var detail = _context.TrBuyerTransactionDetails
                .Include(d => d.Product)
                .FirstOrDefault(d => d.IdBuyerTransactionDetail == transactionDetailId && d.IsActive);

            if (detail == null)
                return NotFound(new { message = "Detail transaksi tidak ditemukan." });

            // Simpan rating & komentar
            detail.Rating = request.Rating;
            detail.RatingComment = request.Comment;
            detail.DateUp = DateTime.Now;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Ulasan berhasil disimpan.",
                product = detail.Product!.ProductName,
                rating = detail.Rating,
                comment = detail.RatingComment
            });
        }

        [HttpDelete("{transactionDetailId}/review")]
        public IActionResult DeleteReview(string transactionDetailId)
        {
            var detail = _context.TrBuyerTransactionDetails
                .Include(d => d.Product)
                .FirstOrDefault(d => d.IdBuyerTransactionDetail == transactionDetailId && d.IsActive);

            if (detail == null)
                return NotFound(new { message = "Detail transaksi tidak ditemukan." });

            detail.Rating = null;
            detail.RatingComment = null;
            detail.DateUp = DateTime.Now;
            _context.SaveChanges();

            return Ok(new
            {
                message = "Ulasan berhasil dihapus.",
                product = detail.Product!.ProductName
            });
        }
    }
}
