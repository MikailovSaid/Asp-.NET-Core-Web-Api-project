using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Core.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile Photo { get; set; }
    }
}
