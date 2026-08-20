using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;

namespace AuthService.Application.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<TokenDTO> Authenticate(UserLoginDTO loginDto)
    {
        var user = await _userRepository.FindByEmail(loginDto.Email);

        if (user == null)
            throw new Exception("Неверный email или пароль");

        if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            throw new Exception("Неверный email или пароль");

        if (!user.IsActive)
            throw new Exception("Пользователь заблокирован");

        var tokenUser = _tokenService.CreateToken(user);
        
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = Convert.ToHexString(
                System.Security.Cryptography.SHA256.HashData(
                    System.Text.Encoding.UTF8.GetBytes(tokenUser.RefreshToken))),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.CreateRefreshToken(refreshToken);
        
        return tokenUser;
    }
}