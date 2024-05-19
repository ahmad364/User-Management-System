using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Application.DTOs;

namespace UserManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> AddUserAsync(UserDto user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
