using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPrep.Api.Repositories
{
    public interface SubtitleSourceRepository : ISubtitleSourceRepository
    {
        private readonly MedPrepContext context;

        public SubtitleSourceRepository(MedPrepContext context)
        {
            this.context = context;
        }

        public SubtitleSource GetSubtitleSourceById(string Name)
        {
            return context.SubtitleSources.Find(Name);
        }

        public IEnumerable<SubtitleSource> GetAllSubtitleSources()
        {
            return context.SubtitleSources.ToList();
        }

        public void DeleteSubtitileSource(SubtitleSource subtitleSource)
        {
            context.SubtitleSources.Remove(subtitleSource);
        
        }

        public void CreateSubtitleSource(SubtitleSource subtitleSource)
        {
            context.SubtitleSources.Add(subtitleSource);

        }

        public void RouteToS3(SubtitleSource subtitleSource)
        {
            // Logic to route the subtitle source to S3 storage
            // This method is just a placeholder and would have actual implementation
        }
    }
}
