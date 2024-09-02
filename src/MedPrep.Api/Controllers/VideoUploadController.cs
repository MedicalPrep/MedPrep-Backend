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
}
