namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class VideoSourceRepository(MedPrepContext context) : IVideoSourceRepository
{
    private readonly MedPrepContext context = context;

    public async Task<VideoSource?> SaveAsync(VideoSource videoSource)
    {
        EntityEntry<VideoSource> entry;
        if (this.context.VideoSource.Any(t => t.Id == videoSource.Id))
        {
            entry = this.context.VideoSource.Update(videoSource);
        }
        else
        {
            entry = await this.context.VideoSource.AddAsync(videoSource);
        }

        return entry.Entity;
    }

    public Task<VideoSource?> GetByIdAsync(Guid videoSourceId) =>
        Task.FromResult(this.context.VideoSource.FirstOrDefault(vs => vs.Id == videoSourceId));

    public Task DeleteAsync(VideoSource videoSource)
    {
        if (this.context.VideoSource.Any(t => t.Id == videoSource.Id))
        {
            _ = this.context.VideoSource.Remove(videoSource);
        }
        return Task.CompletedTask;
    }
}
