namespace AuthService.Application.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword (string password);
    public bool VerifyPassword (string password, string compareHashPassword);
}