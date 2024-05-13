namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;

public class SubtitleSourceRepository(MedPrepContext context) : ISubtitleSourceRepository
{
    private readonly MedPrepContext context = context;

    public SubtitleSource? GetSubtitleSourceById(string name) =>
        this.context.SubtitleSource.Find(name);

    public IEnumerable<SubtitleSource> GetAllSubtitleSources() =>
        this.context.SubtitleSource.ToList();

    public void DeleteSubtitileSource(SubtitleSource subtitleSource) =>
        _ = this.context.SubtitleSource.Remove(subtitleSource);

    public void CreateSubtitleSource(SubtitleSource subtitleSource) =>
        _ = this.context.SubtitleSource.Add(subtitleSource);

    public void RouteToS3(SubtitleSource subtitleSource) => throw new NotImplementedException();
}
