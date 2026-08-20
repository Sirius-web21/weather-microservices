using AuthService.Domain.Enums;

namespace AuthService.Application.DTOs;

public class RegisterUserDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}