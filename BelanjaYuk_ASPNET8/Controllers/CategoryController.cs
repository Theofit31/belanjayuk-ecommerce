using BelanjaYuk.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk_ASPNET8.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.LtCategories
                .Where(c => c.IsActive)
                .Select(c => new { c.IdCategory, c.CategoryName })
                .ToListAsync();

            return Ok(categories);
        }

    }
}
