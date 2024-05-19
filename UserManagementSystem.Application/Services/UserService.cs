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
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IValidator<User> _UserValidator;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDto> userDtoValidator, IValidator<User> UserValidator)
        {
            _userRepository = userRepository;
            _userValidator = userDtoValidator;
            _UserValidator = UserValidator;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> AddUserAsync(UserDto user)
        {
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            User userEntiry = _mapper.Map<User>(user);
            await _userRepository.AddUserAsync(userEntiry);

            return userEntiry;
        }

        public async Task UpdateUserAsync(User user)
        {
            var validationResult = await _UserValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
    }
}
