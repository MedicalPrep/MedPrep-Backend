namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface IUserRepository
{
    Task<User?> GetbyEmailAsync(string email);
    Task<User?> GetbyUsernameAsync(string username);
    Task<User?> GetbyUsernameOrEmailAsync(string usernameOrEmail);
    Task<bool> CheckUsernameAsync(string username);
    Task<bool> CheckEmailAsync(string email);
    Task<User?> GetbyIdAsync(Guid id);
    Task<User?> SaveAsync(User user);
    Task DeleteAsync(User user);
    Task UpdateAsync(User user);
    Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, string refreshToken);
}
