namespace MedPrep.Api.Services.Contracts;

public static class AuthServiceContracts
{
    public record AuthUserResult(
        Guid Id,
        string Email,
        string Username,
        string AccessToken,
        DateTimeOffset AccessTokenExpiration,
        string RefreshToken,
        DateTimeOffset RefreshTokenExpiration
    );

    public record AuthTeacherResult(
        Guid Id,
        string Email,
        string AccessToken,
        DateTimeOffset AccessTokenExpiration,
        string RefreshToken,
        DateTimeOffset RefreshTokenExpiration
    );

    public record LoginQuery(string EmailOrUsername, string Password);

    public record RefreshQuery(string RefreshToken);

    public record RegisterUserCommand(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );

    public record RegisterTeacherCommand(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );

    public record ConfirmAccountQuery(Guid UserId, string Token);
}
