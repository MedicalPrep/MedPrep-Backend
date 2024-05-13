namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface IVideoRepository
{
    Task<Video?> GetByIdAsync(Guid videoId);
    Task<IEnumerable<Video>> GetAllAsync();
    Task<Video?> SaveAsync(Video video);
    Task UpdateAsync(Video video);
    Task DeleteAsync(Video video);
    Task<IEnumerable<User>> GetPurchasersAsync(Guid videoId);
    Task<IEnumerable<SubtitleSource>> GetSubtitleSourcesAsync(Guid videoId);
}
