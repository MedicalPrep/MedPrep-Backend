namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface IVideoSourceRepository
{
    Task<VideoSource?> SaveAsync(VideoSource videoSource);
    Task<VideoSource?> GetByIdAsync(Guid videoSourceId);
    Task DeleteAsync(VideoSource videoSource);
}
