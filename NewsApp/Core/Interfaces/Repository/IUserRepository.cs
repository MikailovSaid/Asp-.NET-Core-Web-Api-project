using NewsApp.Core.Dtos;
using NewsApp.Core.Models;

namespace NewsApp.Core.Interfaces.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<UserOutputDto?> GetUserByIdAsync(int userId);
    }
}
