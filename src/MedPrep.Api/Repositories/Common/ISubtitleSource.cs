namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface ISubtitleSourceRepository
{
    SubtitleSource? GetSubtitleSourceById(string name);
    IEnumerable<SubtitleSource> GetAllSubtitleSources();
    void DeleteSubtitileSource(SubtitleSource subtitleSource);
    void CreateSubtitleSource(SubtitleSource subtitleSource);
    void RouteToS3(SubtitleSource subtitleSource);
}
