using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Dtos;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Interfaces.Repository;
using NewsApp.Core.Interfaces.Services;
using NewsApp.Core.Models;
using NewsApp.Utilities.Auth;
using NewsApp.Utilities.File;
using System.Xml.Linq;

namespace NewsApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> ChangeUsersAgeAsync(int userId, int age)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.Age = age;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersEmailAsync(int userId, string email)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.Email = email;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersNameAndSurnameAsync(int userId, string name, string surname)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.Name = name;
            user.Surname = surname;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersPasswordAsync(int userId, string oldPassword, string password, string confirmPassword)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            if (user.Password != oldPassword)
            {
                throw new ArgumentException("Password is wrong");
            }
            if (password != confirmPassword)
            {
                throw new ArgumentException("Passwords don't match");
            }

            user.Password = password;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersPhotoAsync(int userId, IFormFile photo)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new ArgumentException("User was not found");

            }

            if (!photo.CheckFileType("image/"))
            {
                throw new ArgumentException("File's type is wrong");
            }

            if (!photo.CheckFileSize(1800))
            {
                throw new ArgumentException("File's size is wrong");
            }

            if (photo == null)
            {
                throw new ArgumentException("Add photo");
            }

            System.IO.File.Delete(user.PhotoPath);
            user.PhotoPath = photo.FileName;

            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersRoleAsync(int userId, int roleId)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null || roleId <= 0)
            {
                throw new ArgumentException("User not found");
            }

            user.UserRoleId = roleId;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> ChangeUsersUsernameAsync(int userId, string username)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.Username = username;
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<int> CreateNewUserAsync(UserCreateDto userCreateDto)
        {
            if (!userCreateDto.Photo.CheckFileType("image/"))
            {
                throw new ArgumentException($"File type is wrong. Actual one is {userCreateDto.Photo.ContentType}");
            }

            if (!userCreateDto.Photo.CheckFileSize(1800))
            {
                throw new ArgumentException($"Photo size doesn't match max size value");
            }

            if (userCreateDto.Password != userCreateDto.ConfirmPassword)
            {
                throw new ArgumentException($"Password doesn't match security policy");
            }

            User newUser = new()
            {
                PhotoPath = userCreateDto.Photo.FileName,
                Name = userCreateDto.Name,
                Surname = userCreateDto.Surname,
                Age = userCreateDto.Age,
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                Password = HashPassword.HashPass(userCreateDto.Password),
                UserRoleId = 1
            };

            await _userRepository.InsertAsync(newUser);
            await _unitOfWork.SaveAsync();
            return newUser.Id;
        }

        public async Task<User> DeleteUserAsync(int userId)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            User[] users = await _userRepository.GetAllAsync();
            return users;
        }

        public async Task<UserOutputDto> GetUserProfileByIdAsync(int userId)
        {
            UserOutputDto? userDto = await _userRepository.GetUserByIdAsync(userId);
            if (userDto == null)
            {
                throw new Exception($"User profile for ID: {userId} was not found");
            }

            return userDto;
        }
    }
}
