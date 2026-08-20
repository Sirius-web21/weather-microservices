using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRoles  Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive  { get; set; }
}