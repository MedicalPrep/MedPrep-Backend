namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class CourseModuleRepository(MedPrepContext context) : ICourseModuleRepository
{
    private readonly MedPrepContext context = context;

    public IEnumerable<CourseModule> FindAll() => this.context.CourseModule.ToList();

    public async Task<CourseModule?> SaveAsync(CourseModule courseModule)
    {
        EntityEntry<CourseModule> entry;
        if (this.context.CourseModule.Any(t => t.Id == courseModule.Id))
        {
            entry = this.context.CourseModule.Update(courseModule);
        }
        else
        {
            entry = await this.context.CourseModule.AddAsync(courseModule);
        }

        return entry.Entity;
    }

    public void UpdateCourseModule(CourseModule courseModule)
    {
        // Check if the module with the same ID exists in the database
        var existingModule = this.context.CourseModule.Find(courseModule.Id);
        if (existingModule != null)
        {
            // Update the existing module's properties with the new values
            existingModule.Currency = courseModule.Currency;
            existingModule.Price = courseModule.Price;
            existingModule.Topic = courseModule.Topic;
            existingModule.Type = courseModule.Type;
            // Update any other properties as needed

            // Mark the entity as modified in the context
        }
    }

    public Task<Playlist?> GetPlaylist(CourseModule courseModule)
    {
        if (courseModule.PlaylistId == null)
        {
            return Task.FromResult<Playlist?>(null);
        }
        return Task.FromResult<Playlist?>(
            this.context.Playlist.Where(p => p.Id == courseModule.PlaylistId).First()
        );
    }

    public Task DeleteAsync(CourseModule courseModule)
    {
        if (this.context.CourseModule.Any(t => t.Id == courseModule.Id))
        {
            _ = this.context.CourseModule.Remove(courseModule);
        }

        return Task.CompletedTask;
    }

    public IEnumerable<CourseModule> FindByTopic(string topic) =>
        throw new NotImplementedException();

    public bool CourseModuleExists(CourseModule courseModule) => this.context.CourseModule.Any(t => t.Id == courseModule.Id);

}

