using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;

namespace AuthService.Application.Services;

public class RegistrationService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    
    public RegistrationService(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponseDto> Register(RegisterUserDTO userDto)
    {
        var existedUser = await _userRepository.FindByEmail(userDto.Email);
        if (existedUser != null)
            throw new Exception("Пользователь с таким email уже существует");

        var hash = _passwordHasher.HashPassword(userDto.Password);

        var user = new User
        {
            Email = userDto.Email,
            PasswordHash = hash,
            IsActive = true,
            Role = UserRoles.Manager,
            CreatedAt =  DateTime.UtcNow
        };
        
        await _userRepository.Create(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            RoleName = user.Role.ToString()
        };
  
    }
}