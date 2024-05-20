using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Domain.Entities;
using FluentValidation;
using UserManagementSystem.Application.DTOs;

namespace UserManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor injection for the user service
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users/GetUsersList
        [Route("GetUsersList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        // GET: api/Users/GetUserById/{id}
        [Route("GetUserById/{id}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User ID is Invalid.");

            return Ok(user);
        }

        // POST: api/Users/CreateUser
        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto userDto)
        {
            var user = await _userService.AddUserAsync(userDto);
            // Returns a 201 Created response with the newly created user
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // PUT: api/Users/UpdateUser/{id}
        [Route("UpdateUser/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User ID is Invalid.");

            // Update user properties
            user.CNIC = userDto.CNIC;
            user.UserName = userDto.UserName;
            user.PhoneNumber = userDto.PhoneNumber;

            await _userService.UpdateUserAsync(user);
            return Ok("User details have been updated successfully.");
        }

        // DELETE: api/Users/DeleteUser/{id}
        [Route("DeleteUser/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User ID is Invalid.");

            await _userService.DeleteUserAsync(user);
            return Ok("User has been successfully deleted.");
        }
    }
}
