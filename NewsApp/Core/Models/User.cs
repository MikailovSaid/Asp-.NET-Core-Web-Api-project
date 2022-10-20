using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public string? Token { get; set; }
    }
}
