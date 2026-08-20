using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces;

public interface IUserRepository
{
    public Task Create (User user);
    public Task <User?> FindByEmail(string email);
    public Task <User?> FindById(Guid id);
}