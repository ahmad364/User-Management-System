using FluentValidation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Domain.Interfaces;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Application.DTOs;

namespace UserManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        // Private readonly fields for dependencies
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IValidator<User> _UserValidator;
        private readonly IMapper _mapper;

        // Constructor to initialize dependencies
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDto> userDtoValidator, IValidator<User> UserValidator)
        {
            _userRepository = userRepository;
            _userValidator = userDtoValidator;
            _UserValidator = UserValidator;
            _mapper = mapper;
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // Get all users
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Add a new user
        public async Task<User> AddUserAsync(UserDto user)
        {
            // Validate the user DTO
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map UserDto to User entity and add it to the repository
            User userEntiry = _mapper.Map<User>(user);
            await _userRepository.AddUserAsync(userEntiry);

            return userEntiry;
        }

        // Update an existing user
        public async Task UpdateUserAsync(User user)
        {
            // Validate the user entity
            var validationResult = await _UserValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Update the user in the repository
            await _userRepository.UpdateUserAsync(user);
        }

        // Delete a user
        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
    }
}
