using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces;

public interface IRefreshTokenRepository
{
    public Task CreateRefreshToken (RefreshToken token);
    public Task RevokeRefreshToken (RefreshToken token);
    public Task<RefreshToken?> FindRefreshToken (string hashToken);
}