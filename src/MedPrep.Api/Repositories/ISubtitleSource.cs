using System;
using System.Collections.Generic;
using MedPrep.Api.Models;

using System.Collections.Generic;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;

namespace MedPrep.Api.Repositories
{
    public interface ISubtitleSourceRepository : IDisposable, ISoftDeletable
    {
        SubtitleSource GetSubtitleSourceById(string Name);
        IEnumerable<SubtitleSource> GetAllSubtitleSources();
        void DeleteSubtitileSource(SubtitleSource subtitleSource);
        void CreateSubtitleSource(SubtitleSource subtitleSource);
        void RouteToS3(SubtitleSource subtitleSource);

        void Save();
}
