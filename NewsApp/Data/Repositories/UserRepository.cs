using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Dtos;
using NewsApp.Core.Interfaces.Repository;
using NewsApp.Core.Models;
using NewsApp.Data;

namespace NewsApp.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<UserOutputDto?> GetUserByIdAsync(int userId)
        {
            return Set.Where(u => u.Id == userId)
                 .Select(usr => new UserOutputDto
                 {
                     Id = usr.Id,
                     PhotoPath = usr.PhotoPath,
                     Name = usr.Name,
                     Surname = usr.Surname,
                     Age = usr.Age,
                     Email = usr.Email,
                     Username = usr.Username,
                     Password = usr.Password,
                     UserRoleId = usr.UserRoleId
                 })
                 .FirstOrDefaultAsync();
        }
    }
}
