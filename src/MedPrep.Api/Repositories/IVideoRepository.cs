using System;
using System.Collections.Generic;
using MedPrep.Api.Models;

using System;
using System.Collections.Generic;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;

namespace MedPrep.Api.Repositories
{
    public interface IVideoRepository : IDisposable, ISoftDeletable
    {
        Video GetVideoByTile(string Name);
        IEnumerable<Video> GetAllVideos();
        void CreateVideo(Video video);
        void UpdateVideo(Video video);
        void DeleteVideo(string Name);
        IEnumerable<User> GetUsersPurchasedVideo(string videoId);
        SubtitleSource GetSubtitleSourceForVideo(string videoId);

    }
}
