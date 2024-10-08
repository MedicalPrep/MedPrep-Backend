namespace MedPrep.Api.Services.Common;

using System.Threading.Tasks;
using static MedPrep.Api.Services.Contracts.IVideoServiceContracts;

public interface IVideoService
{
    Task<VideoUploadResult> UploadVideo(VideoUploadCommand command);
}
