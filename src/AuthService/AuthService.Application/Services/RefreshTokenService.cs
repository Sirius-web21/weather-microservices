using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;

namespace AuthService.Application.Services;

public class RefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Refresh(string refreshToken)
    {
        var hash = Convert.ToHexString(
            System.Security.Cryptography.SHA256.HashData(
                System.Text.Encoding.UTF8.GetBytes(refreshToken)));

        var storedToken =
            await _refreshTokenRepository.FindRefreshToken(hash);

        if (storedToken == null)
            throw new Exception("Refresh token не найден");

        if (storedToken.RevokedAt.HasValue)
            throw new Exception("Refresh token отозван");

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
            throw new Exception("Refresh token истёк");


        throw new NotImplementedException();
    }
}