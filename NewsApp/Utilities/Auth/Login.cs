using NewsApp.Core.Dtos;
using NewsApp.Core.Models;
using NewsApp.Data;
using System.Security.Cryptography;
using System.Text;

namespace NewsApp.Utilities.Auth
{
    public class Login
    {
        private readonly AppDbContext _context;
        public Login(AppDbContext context)
        {
            _context = context;
        }

        public User AuthenticateUser(LoginReqDto login)
        {
            try
            {
                User user = _context.Users
                    .FirstOrDefault(I => I.Email == login.Email && I.Password == HashPassword.HashPass(login.Password));

                return user;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
