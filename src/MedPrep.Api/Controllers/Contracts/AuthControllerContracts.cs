namespace MedPrep.Api.Controllers.Contracts;

public static class AuthControllerContracts
{
    public record LoginRequest(string EmailOrUsername, string Password);

    public record LoginUserResponse(
        Guid Id,
        string Email,
        string Username,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record LoginTeacherResponse(
        Guid Id,
        string Email,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record RegisterTeacherRequest(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );

    public record RegisterUserRequest(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );

    public record RefreshTokenRequest(string RefreshToken);

    public record RefreshUserTokenResponse(
        Guid Id,
        string Email,
        string Username,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record RefreshTeacherTokenResponse(
        Guid Id,
        string Email,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record ConfirmAccountRequest(Guid UserId, string Token);
}
