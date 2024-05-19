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

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetUsersList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [Route("GetUserById/{id}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User ID is Invalid.");

            return Ok(user);
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto userDto)
        {
            var user = await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [Route("UpdateUser/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User ID is Invalid.");

            user.CNIC = userDto.CNIC;
            user.UserName = userDto.UserName;
            user.PhoneNumber = userDto.PhoneNumber;

            await _userService.UpdateUserAsync(user);
            return Ok("User details has been updated successfully.");
        }


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
