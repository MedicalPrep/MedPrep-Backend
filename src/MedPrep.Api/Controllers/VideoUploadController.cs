namespace MedPrep.Api.Controllers;

using System.Threading.Tasks;
using MedPrep.Api.Services.Common;
using Microsoft.AspNetCore.Mvc;
using static MedPrep.Api.Controllers.Contracts.VideoUploadControllerContracts;
using static MedPrep.Api.Services.Contracts.IVideoServiceContracts;

[ApiController]
[Route("api/v1/video")]
public class VideoUploadController(IVideoService videoService) : Controller
{
    private readonly IVideoService videoService = videoService;

    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VideoUploadResponse>> UploadVideo([FromForm] VideoUploadRequest videoUpload)
    {
        var command = new VideoUploadCommand()
        {
            Title = videoUpload.Title,
            Description = videoUpload.Description,
            CourseModuleId = videoUpload.CourseModuleId
        };
        var result = await this.videoService.UploadVideo(command);

        var response = new VideoUploadResponse()
        {
            Title = result.Title,
            UploadEndpoint = result.UploadEndpoint,
            ThirdPartyVideoId = result.ThirdPartyVideoId,
            ThirdPartyVideoCollectionId = result.ThirdPartyVideoCollectionId,
            ThirdPartyLibraryId = result.ThirdPartyLibraryId,
            AuthorizationSignature = result.AuthorizationSignature,
            AuthorizationExpiration = result.AuthorizationExpiration,
        };

        return this.Ok(response);
    }

    [HttpGet]
    [Route("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VideoRequestResponse>> GetVideoById([FromRoute] Guid id)
    {
        var video = await this.videoService.FetchVideoInfo(id);

        if (video == null)
        {
            return this.NotFound();
        }

        // Map service response to VideoResponse DTO
        var videoResponse = new VideoRequestResponse
        {
            Title = video.Title,
            Description = video.Description,
            NextVideo = video.NextVideo,
            PrevVideo = video.PrevVideo,
            VideoSource = video.VideoSource,
            SubtitleSource = video.SubtitleSource,
            CourseModule = video.CourseModule != null ? new CourseModuleDto
            {
                Id = video.CourseModule.Id,
                Name = video.CourseModule.Name
            } : null,
            Playlist = video.Playlist != null ? new PlaylistDto
            {
                Id = video.Playlist.Id,
                Name = video.Playlist.Name
            } : null
        };

        return this.Ok(videoResponse);
}


}
