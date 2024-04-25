using System;
using System.Collections.Generic;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;



namespace MedPrep.Api.Repositories
{
    public interface ICourseModuleRepository
    {
        void CreateCourseModule(CourseModule module);
        // CourseModule GetCourseModuleById(int moduleId);
        void UpdateCourseModule(CourseModule module);

        void DeleteCourseModule(CourseModule module);
        IEnumerable<CourseModule> GetAllCourseModules();
        IEnumerable<CourseModule> GetCourseModulesByTopic(string topic);
        Playlist GetPlaylistForCourseModule(CourseModule module);
        double GetCurrentCourseModulePrice(CourseModule module);

        void Save();
    }
}
