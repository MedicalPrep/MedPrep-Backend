namespace MedPrep.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MedPrep.Api.Services.Common;
using static MedPrep.Api.Services.Contracts.IVideoServiceContracts;




[ApiController]
[Route("api/v1/video")]
public class VideoUploadController : Controller
{
    public readonly IVideoService _iVideoService;
    public VideoUploadController(IVideoService iVideoService)
    {
        this._iVideoService = iVideoService;
    }

    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UploadVideo([FromForm] VideoUploadRequest videoUpload)
    {
        try
        {
            var result = await _iVideoService.UploadVideo(videoUpload);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }


}
