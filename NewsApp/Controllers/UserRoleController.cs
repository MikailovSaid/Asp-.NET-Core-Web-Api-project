using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Models;
using NewsApp.Data;

namespace NewsApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserRoleController : BaseApiController
    {
        private readonly AppDbContext _context;
        public UserRoleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-role-by-id")]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            UserRole? role = await _context.UserRoles.FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role.Role);
        }

        [HttpGet("get-role-by-user-id")]
        public async Task<IActionResult> GetUserRoleByUserId(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserRole role = await _context.UserRoles.FirstOrDefaultAsync(m => m.Id == user.UserRoleId);
            return Ok(role.Role);
        }

        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetAllUserRoles()
        {
            List<UserRole>? roles = await _context.UserRoles.ToListAsync();
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles.Select(x => x.Role).ToList());
        }
    }
}
