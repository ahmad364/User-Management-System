using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Application.Services;
using UserManagementSystem.Domain.Interfaces;
using UserManagementSystem.Infrastructure.Repositories;
using UserManagementSystem.Infrastructure;
using UserManagementSystem.Application.Validator;
using UserManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Infrastructure.DBContext;
using UserManagementSystem.Application.DTOs;
using UserManagementSystem.Application.Mapping_Profiles;
using UserManagementSystem.API.Middlewares;
using UserManagementSystem.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("ConnStr");
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("UserManagementSystem.API"));
});

// Register application services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();

// Register AutoMapper with mapping profiles
builder.Services.AddAutoMapper(typeof(UserMapperProfile));

var app = builder.Build();

// Use custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
