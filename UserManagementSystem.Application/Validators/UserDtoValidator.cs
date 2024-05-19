using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Application.DTOs;
namespace UserManagementSystem.Application.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(u => u.CNIC).NotEmpty().Matches(@"^\d{13}$").WithMessage("CNIC must be a 13 digit number.");
            RuleFor(u => u.PhoneNumber).NotEmpty().Matches(@"^\d{11}$").WithMessage("Phone number must be an 11 digit number.");
        }
    }
}
