using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UserManagementSystem.Application.DTOs;
using UserManagementSystem.Domain.Entities;
namespace UserManagementSystem.Application.Mapping_Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()); // Ignore UserId during creation
        }
    }
}
