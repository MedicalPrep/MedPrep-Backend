namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        this.context.User.FirstOrDefaultAsync(t => t.Username == username);

    public Task<User?> GetbyUsernameOrEmailAsync(string usernameOrEmail) =>
        this.context.User.FirstOrDefaultAsync(t =>
            t.Username == usernameOrEmail || t.Email == usernameOrEmail
        );

    public Task<User?> GetbyIdAsync(Guid id) =>
        this.context.User.FirstOrDefaultAsync(t => t.Id == id);

    public Task<bool> CheckUsernameAsync(string username) =>
        this.context.User.AnyAsync(u => u.Username == username);

    public Task<bool> CheckEmailAsync(string email) => this.context.User.AnyAsync(u => u.Email == email);

    public async Task<User?> SaveAsync(User user)
    {
        if (this.context.User.Any(t => t.Id == user.Id))
        {
            _ = this.context.User.Update(user);
        }
        else
        {
            _ = await this.context.User.AddAsync(user);
        }

        return user;
    }

    public Task UpdateAsync(User user)
    {
        if (this.context.User.Any(t => t.Id == user.Id))
        {
            _ = this.context.User.Update(user);
        }
        return Task.CompletedTask;
    }
}
