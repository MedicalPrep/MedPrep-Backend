using System;
using System.Collections.Generic;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;

namespace MedPrep.Api.Repositories{

    public interface IVideoSourceRepository :
    {
        public void GetVideoSource(VideoSource videoSource, Video video);

        public void CreateVideoSource(VideoSource videoSource);
        public void DeleteVideoSource(VideoSource videoSource, Video video);

        public void GetDateDeleted(VideoSource videoSource, Video video);

        public void UpdateVideoSource(VideoSource videoSource, VideoSource UpdatedvideoSource);

        IEnumerable<VideoSource> GetallVideoSources(VideoSource videoSource);


    }
}
