using System;
using System.Collections.Generic;
using MedPrep.Api.Models;
using System.Linq;

namespace MedPrep.Api.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly MedPrepContext context;

        public VideoRepository(MedPrepContext context)
        {
            this.context = context;
        }

        public Video GetVideoById(string Name)
        {
            return context.Videos.Find(Name);
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return context.Videos.ToList();
        }

        public void CreateVideo(Video video)
        {
            context.Videos.Add(video);
            Save();
        }

        public void UpdateVideo(Video video)
        {
            context.Videos.Update(video);
            Save();
        }

        public void DeleteVideo(string Name)
        {
            var video = GetVideoById(Name);
            if (video != null)
            {
                context.Videos.Remove(video);
                Save();
            }
        }

        public IEnumerable<User> GetUsersPurchasedVideo(string videoId)
        {
            return context.Videos
                .Where(v => v.Title == videoId)
                .SelectMany(v => v.Purchasers)
                .ToList();
        }

        public SubtitleSource GetSubtitleSourceForVideo(sting Name)
        {
            var video = GetVideoById(videoId);
            return video?.SubtitleSources.FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}

