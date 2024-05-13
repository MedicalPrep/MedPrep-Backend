namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class UserRepository(MedPrepContext context) : IUserRepository
{
    private readonly MedPrepContext context = context;

    public Task DeleteAsync(User user)
    {
        if (this.context.User.Any(t => t.Id == user.Id))
        {
            _ = this.context.User.Remove(user);
        }

        return Task.CompletedTask;
    }

    public Task<User?> GetbyEmailAsync(string email) =>
        this.context.User.FirstOrDefaultAsync(t => t.Email == email);

    public Task<User?> GetbyUsernameAsync(string username) =>
        this.context.User.FirstOrDefaultAsync(t => t.UserName == username);

    public Task<User?> GetbyUsernameOrEmailAsync(string usernameOrEmail) =>
        this.context.User.FirstOrDefaultAsync(t =>
            t.UserName == usernameOrEmail || t.Email == usernameOrEmail
        );

    public Task<User?> GetbyIdAsync(Guid id) =>
        this.context.User.FirstOrDefaultAsync(t => t.Id == id);

    public Task<bool> CheckUsernameAsync(string username) =>
        this.context.User.AnyAsync(u => u.UserName == username);

    public Task<bool> CheckEmailAsync(string email) =>
        this.context.User.AnyAsync(u => u.Email == email);

    public async Task<User?> SaveAsync(User user)
    {
        EntityEntry<User> entry;
        if (this.context.User.Any(t => t.Id == user.Id))
        {
            entry = this.context.User.Update(user);
        }
        else
        {
            entry = await this.context.User.AddAsync(user);
        }

        return entry.Entity;
    }

    public Task UpdateAsync(User user)
    {
        if (this.context.User.Any(t => t.Id == user.Id))
        {
            _ = this.context.User.Update(user);
        }
        return Task.CompletedTask;
    }

    public Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, string refreshToken) =>
        this
            .context.RefreshToken.Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.AccountId == userId && t.Token == refreshToken);
}
