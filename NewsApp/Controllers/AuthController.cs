using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsApp.Core.Dtos;
using NewsApp.Core.Models;
using NewsApp.Data;
using NewsApp.Utilities.Auth;

namespace NewsApp.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly AppDbContext _context;
        IConfiguration _configuration;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginReqDto request)
        {
            Login auth = new Login(_context);
            WebToken token = new WebToken(_configuration);
            LoginResDto response = new LoginResDto();
            var user = auth.AuthenticateUser(request);
            if (user != null)
            {
                string RToken = Guid.NewGuid().ToString();
                user.Token = RToken;
                _context.Users.Update(user);
                _context.LoginLogs.Add(new LoginLog
                {
                    DateTime = DateTime.Now,
                    UserId = user.Id,
                    IP = request.IP
                });
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                response.RefreshToken = RToken;
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
