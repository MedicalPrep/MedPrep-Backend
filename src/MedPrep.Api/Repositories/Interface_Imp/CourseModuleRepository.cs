using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MedPrep.Api.Repositories
{
    public class CourseModuleRepository : ICourseModuleRepository
    {

        private readonly MedPrepContext context;

        public CourseModuleRepository(MedPrepContext context){
            this.context = context
        }

        public IEnumerable<CourseModule> GetAllCourseModules(){
            return context.CourseModules.ToList();
        }

        public void CreateCourseModule(CourseModule module){
            context.CourseModules.Add(module);
        }

    public void UpdateCourseModule(CourseModule module)
    {
        // Check if the module with the same ID exists in the database
        var existingModule = context.CourseModules.Find(module.Id);
        if (existingModule != null)
        {
            // Update the existing module's properties with the new values
            existingModule.Currency = module.Currency;
            existingModule.Price = module.Price;
            existingModule.Topic = module.Topic;
            existingModule.Type = module.Type;
            // Update any other properties as needed

            // Mark the entity as modified in the context
            context.Entry(existingModule).State = EntityState.Modified;


        }
    }

    public Playlist GetPlaylistForCourseModule(CourseModule module)
        {
            return module.Playlist
        }

    public double GetCurrentCourseModulePrice(CourseModule module)
        {
            return Convert.ToDouble(module.Price);
        }
    }
}
