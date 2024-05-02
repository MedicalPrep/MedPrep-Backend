namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface ITeacherRepository
{
    Task<Teacher?> GetbyEmailAsync(string email);
    Task<Teacher?> GetbyIdAsync(Guid id);
    Task<Teacher?> SaveAsync(Teacher teacher);
    Task<bool> CheckEmailAsync(string email);
    Task DeleteAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
}
