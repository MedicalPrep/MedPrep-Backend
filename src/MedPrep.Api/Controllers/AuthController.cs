namespace MedPrep.Api.Controllers;

using MedPrep.Api.Services.Common;
using Microsoft.AspNetCore.Mvc;
using static MedPrep.Api.Controllers.Contracts.AuthControllerContracts;
using static MedPrep.Api.Services.Contracts.AuthServiceContracts;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService authService = authService;

    [HttpPost("login/user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginUserResponse>> LoginUser([FromBody] LoginRequest request)
    {
        var query = new LoginQuery(request.EmailOrUsername, request.Password);
        var result = await this.authService.LoginUser(query);
        var response = new LoginUserResponse(
            result.Id,
            result.Email,
            result.Username,
            result.AccessToken,
            result.AccessTokenExpiration,
            result.RefreshToken,
            result.RefreshTokenExpiration
        );

        return this.Ok(response);
    }

    [HttpPost("login/teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginUserResponse>> LoginTeacher([FromBody] LoginRequest request)
    {
        var query = new LoginQuery(request.EmailOrUsername, request.Password);
        var result = await this.authService.LoginTeacher(query);
        var response = new LoginTeacherResponse(
            result.Id,
            result.Email,
            result.AccessToken,
            result.AccessTokenExpiration,
            result.RefreshToken,
            result.RefreshTokenExpiration
        );

        return this.Ok(response);
    }

    [HttpPost("register/teacher")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RegisterTeacher([FromBody] RegisterTeacherRequest request)
    {
        var command = new RegisterTeacherCommand(
            request.Email,
            request.Username,
            request.Password,
            request.Firstname,
            request.Lastname
        );

        await this.authService.RegisterTeacher(command);

        return this.Created("", null);
    }

    [HttpPost("register/user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.Username,
            request.Password,
            request.Firstname,
            request.Lastname
        );

        await this.authService.RegisterUser(command);

        return this.Created("", null);
    }

    [HttpPost("refresh/user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RefreshUserTokenResponse>> RefreshTokenUser(
        [FromBody] RefreshTokenRequest request
    )
    {
        var query = new RefreshQuery(request.RefreshToken);

        var result = await this.authService.RefreshUserToken(query);

        var response = new RefreshUserTokenResponse(
            result.Id,
            result.Email,
            result.Username,
            result.AccessToken,
            result.AccessTokenExpiration,
            result.RefreshToken,
            result.RefreshTokenExpiration
        );

        return this.Ok(response);
    }

    [HttpPost("refresh/teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RefreshTeacherTokenResponse>> RefreshTeacherToken(
        [FromBody] RefreshTokenRequest request
    )
    {
        var query = new RefreshQuery(request.RefreshToken);

        var result = await this.authService.RefreshTeacherToken(query);

        var response = new RefreshTeacherTokenResponse(
            result.Id,
            result.Email,
            result.AccessToken,
            result.AccessTokenExpiration,
            result.RefreshToken,
            result.RefreshTokenExpiration
        );

        return this.Ok(response);
    }

    [HttpGet("confirm/user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ConfirmUser(
        [FromQuery(Name = "id")] Guid userId,
        [FromQuery(Name = "token")] string token
    )
    {
        var query = new ConfirmAccountQuery(userId, token);

        await this.authService.ConfirmUser(query);

        return this.Ok("Email Confirmed!!!");
    }

    [HttpGet("confirm/teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ConfirmTeacher(
        [FromQuery(Name = "id")] Guid userId,
        [FromQuery(Name = "token")] string token
    )
    {
        var query = new ConfirmAccountQuery(userId, token);

        await this.authService.ConfirmTeacher(query);

        return this.Ok("Email Confirmed!!!");
    }
}
