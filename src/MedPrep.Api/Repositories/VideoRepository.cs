namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class VideoRepository(MedPrepContext context) : IVideoRepository
{
    private readonly MedPrepContext context = context;

    public Task<Video?> GetByIdAsync(Guid videoId) =>
        Task.FromResult(this.context.Video.Where(v => v.Id == videoId).FirstOrDefault());

    public Task<IEnumerable<Video>> GetAllAsync() =>
        Task.FromResult<IEnumerable<Video>>(this.context.Video.Select(v => v));

    public async Task<Video?> SaveAsync(Video video)
    {
        EntityEntry<Video> entry;
        if (this.context.Video.Any(t => t.Id == video.Id))
        {
            entry = this.context.Video.Update(video);
        }
        else
        {
            entry = await this.context.Video.AddAsync(video);
        }

        return entry.Entity;
    }

    public Task UpdateAsync(Video video)
    {
        if (this.context.Video.Any(t => t.Id == video.Id))
        {
            _ = this.context.Video.Update(video);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Video video)
    {
        if (this.context.Video.Any(t => t.Id == video.Id))
        {
            _ = this.context.Video.Remove(video);
        }

        return Task.CompletedTask;
    }

    public Task<IEnumerable<User>> GetPurchasersAsync(Guid videoId) =>
        Task.FromResult<IEnumerable<User>>(
            this.context.Video.Where(v => v.Id == videoId).SelectMany(v => v.Purchasers)
        );

    public Task<IEnumerable<SubtitleSource>> GetSubtitleSourcesAsync(Guid videoId) =>
        Task.FromResult<IEnumerable<SubtitleSource>>(
            this.context.SubtitleSource.Where(s => s.VideoId == videoId).Select(x => x)
        );

    public Task<bool> AnyAsync(Func<Video, bool> predicate)
    {
        var result = this.context.Video.Any(predicate);

        return Task.FromResult(result);
    }
}
