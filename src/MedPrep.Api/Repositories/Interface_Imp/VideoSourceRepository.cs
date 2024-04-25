using System;
using System.Collections.Generic;
using System.Linq;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;

namespace MedPrep.Api.Repositories
{
    public class VideoSourceRepository : IVideoSourceRepository
    {
        private readonly MedPrepContext _context;

        public VideoSourceRepository(MedPrepContext context)
        {
            _context = context;
        }

        public void CreateVideoSource(VideoSource videoSource)
        {
            _context.VideoSources.Add(videoSource);
            Save();
        }

        public VideoSource GetVideoSourceById(Guid videoSourceId)
        {
            return _context.VideoSources.FirstOrDefault(vs => vs.VideoId == videoSourceId);
        }

        public IEnumerable<VideoSource> GetVideoSourcesByVideoId(Guid videoId)
        {
            return _context.VideoSources.Where(vs => vs.VideoId == videoId).ToList();
        }

        public void UpdateVideoSource(VideoSource videoSource)
        {
            var existingVideoSource = _context.VideoSources.FirstOrDefault(vs => vs.VideoId == videoSource.Id);
            if (existingVideoSource != null)
            {
                _context.Entry(existingVideoSource).CurrentValues.SetValues(videoSource);
                Save();
            }
        }


        public void DeleteVideoSource(VideoSource videoSource)
        {
            _context.VideoSources.Remove(videoSource);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
