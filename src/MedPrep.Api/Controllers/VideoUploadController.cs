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

    [HttpGet]
    [Route("play/id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VideoPlayResponse>> PlayVideo(Guid videoId)
    {
        // i MADE REFERENCE TO THE SERVICE DTO FOR BOTH OF THE TWO FUNCTIONS, PLEASE CLARIFY,
        //APPARENTLY ITS CONFLICTING FOR BOTH THE SERVICE AND THE CONTROLLER, I.E THE MODLE THE CONTROLLER IS MEANT TO RETURN, I COMMENTED IT OUT

    try
        {
            // Fetch the video details from the BunnyStream service
            var videoPlayResult = await this.videoService.PlayVideo(videoId);

            if (videoPlayResult == null)
            {
                return this.NotFound(); // Return 404 if video not found
            }

            // Map the service result to your DTO
            var videoPlayResponse = new VideoPlayResponse
            {
                Title = videoPlayResult.Title,
                Description = videoPlayResult.Description,
                VideoUrl = videoPlayResult.VideoUrl,
                ThumbnailUrl = videoPlayResult.ThumbnailUrl,
                NextVideo = videoPlayResult.NextVideo,
                PrevVideo = videoPlayResult.PrevVideo,
                SubtitleSource = videoPlayResult.SubtitleSource,
                Playlist = videoPlayResult.Playlist
            };

            // Return the mapped DTO
            return this.Ok(videoPlayResponse); // Return 200 OK with the DTO
        }
        catch (HttpRequestException ex)
        {
            // Handle error (e.g., BunnyStream API failure)
            return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        catch (Exception ex)
        {
            // Catch any other errors and return 500
            return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}



