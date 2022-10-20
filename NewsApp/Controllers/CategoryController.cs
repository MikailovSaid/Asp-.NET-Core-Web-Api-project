using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Models;
using NewsApp.Data;

namespace NewsApp.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Category? category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Name);
        }
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category? category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("edit-category")]
        public async Task<IActionResult> EditCategory(Category category)
        {
            Category? dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == category.Id);
            if (category.Id != dbCategory!.Id)
            {
                return NotFound();
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
