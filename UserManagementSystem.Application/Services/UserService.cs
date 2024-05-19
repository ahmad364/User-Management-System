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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDto> userDtoValidator)
        {
            _userRepository = userRepository;
            _userValidator = userDtoValidator;
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

        public async Task AddUserAsync(UserDto user)
        {
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            User userEntiry = _mapper.Map<User>(user);
            await _userRepository.AddUserAsync(userEntiry);
        }

        public async Task UpdateUserAsync(int userId, UserDto user)
        {
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            User userEntiry = _mapper.Map<User>(user);
            userEntiry.UserId = userId;

            await _userRepository.UpdateUserAsync(userEntiry);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
