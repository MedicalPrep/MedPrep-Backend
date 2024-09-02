namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface ICourseModuleRepository
{
    Task<CourseModule?> SaveAsync(CourseModule courseModule);

    // CourseModule GetCourseModuleById(int moduleId);
    void UpdateCourseModule(CourseModule courseModule);

    Task DeleteAsync(CourseModule courseModule);
    IEnumerable<CourseModule> FindAll();
    IEnumerable<CourseModule> FindByTopic(string topic);
    Task<Playlist?> GetPlaylist(CourseModule courseModule);

    public bool CourseModuleExists(CourseModule courseModule);
}
