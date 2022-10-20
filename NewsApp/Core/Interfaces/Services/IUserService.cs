using NewsApp.Core.Dtos;
using NewsApp.Core.Models;

namespace NewsApp.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<int> CreateNewUserAsync(UserCreateDto userCreateDto);
        Task<User[]> GetAllUsersAsync();
        Task<UserOutputDto> GetUserProfileByIdAsync(int userId);
        Task<User> ChangeUsersRoleAsync(int userId, int roleId);
        Task<User> DeleteUserAsync(int userId);
        Task<User> ChangeUsersNameAndSurnameAsync(int userId, string name, string surname);
        Task<User> ChangeUsersUsernameAsync(int userId, string username);
        Task<User> ChangeUsersEmailAsync(int userId, string email);
        Task<User> ChangeUsersPasswordAsync(int userId, string oldPassword, string password, string confirmPassword);
        Task<User> ChangeUsersAgeAsync(int userId, int age);
        Task<User> ChangeUsersPhotoAsync(int userId, IFormFile photo);
    }
}
