namespace MedPrep.Api.Repositories;

using System.Threading.Tasks;
using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class TeacherRepository(MedPrepContext context) : ITeacherRepository
{
    private readonly MedPrepContext context = context;

    public Task<bool> CheckEmailAsync(string email) =>
        this.context.Teacher.AnyAsync(t => t.Email == email);

    public Task DeleteAsync(Teacher teacher)
    {
        if (this.context.Teacher.Any(t => t.Id == teacher.Id))
        {
            _ = this.context.Teacher.Remove(teacher);
        }

        return Task.CompletedTask;
    }

    public Task<Teacher?> GetbyEmailAsync(string email) =>
        this.context.Teacher.FirstOrDefaultAsync(t => t.Email == email);

    public Task<Teacher?> GetbyIdAsync(Guid id) =>
        this.context.Teacher.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Teacher?> SaveAsync(Teacher teacher)
    {
        if (this.context.Teacher.Any(t => t.Id == teacher.Id))
        {
            _ = this.context.Teacher.Update(teacher);
        }
        else
        {
            _ = await this.context.Teacher.AddAsync(teacher);
        }

        return teacher;
    }

    public Task UpdateAsync(Teacher teacher)
    {
        if (this.context.Teacher.Any(t => t.Id == teacher.Id))
        {
            _ = this.context.Teacher.Update(teacher);
        }
        return Task.CompletedTask;
    }
}
