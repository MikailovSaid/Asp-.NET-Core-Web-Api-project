using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Utilities.File;
using NewsApp.Utilities.Auth;
using Microsoft.AspNetCore.Authorization;
using NewsApp.Core.Dtos;
using NewsApp.Core.Interfaces.Services;
using NewsApp.Core.Models;

namespace NewsApp.Controllers
{
    [Route("v1/api/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        public UserController(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Get user profile by id.
        /// </summary>
        /// <param name="id">User id (int type)</param>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">The user was not found.</response>
        /// <response code="500">Internal Sever Error.</response>
        /// <returns>UserOutputDTO payload.</returns>
        [HttpGet("get-user-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UserOutputDto> GetUserById(int id)
        {
            return await _userService.GetUserProfileByIdAsync(id);
        }

        [HttpGet("get-all-users")]
        public async Task<User[]> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromForm] UserCreateDto user)
        {
            int newUserId = await _userService.CreateNewUserAsync(user);
            return new JsonResult(new { id = newUserId });
        }

        [HttpPut("change-users-role")]
        public async Task<IActionResult> ChangeUsersRole(int id, int roleId)
        {
            User? newUser = await _userService.ChangeUsersRoleAsync(id, roleId);
            return Ok(newUser);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted");
        }

        [HttpPut("change-users-name-and-surname")]
        public async Task<IActionResult> ChangeUsersNameAndSurname(int id, string name, string surname)
        {
            User? user = await _userService.ChangeUsersNameAndSurnameAsync(id, name, surname);
            return Ok(user);
        }

        [HttpPut("change-users-username")]
        public async Task<IActionResult> ChangeUsersUsername(int id, string username)
        {
            User? user = await _userService.ChangeUsersUsernameAsync(id, username);
            return Ok(user);
        }

        [HttpPut("change-users-email")]
        public async Task<IActionResult> ChangeUsersEmail(int id, string email)
        {
            User? user = await _userService.ChangeUsersEmailAsync(id, email);
            return Ok(user);
        }

        [HttpPut("change-users-age")]
        public async Task<IActionResult> ChangeUsersAge(int id, int age)
        {
            User? user = await _userService.ChangeUsersAgeAsync(id, age);
            return Ok(user);
        }

        [HttpPut("change-users-photo")]
        public async Task<IActionResult> ChangeUsersPhoto(int id, IFormFile photo)
        {
            User? user = await _userService.ChangeUsersPhotoAsync(id, photo);
            return Ok(user);
        }

        [HttpPut("change-users-password")]
        public async Task<IActionResult> ChangeUsersPassword(int id, string oldPassword, string password, string confirmPassword)
        {
            User? user = await _userService.ChangeUsersPasswordAsync(id, oldPassword, password, confirmPassword);
            return Ok(user);
        }
    }
}
