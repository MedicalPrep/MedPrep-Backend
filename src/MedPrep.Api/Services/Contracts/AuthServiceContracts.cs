namespace MedPrep.Api.Services.Contracts;

public static class AuthServiceContracts
{
    public record AuthUserResult(
        Guid Id,
        string Email,
        string Username,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record AuthTeacherResult(
        Guid Id,
        string Email,
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );

    public record LoginQuery(string EmailOrUsername, string Password);

    public record RefreshQuery(string RefreshToken);

    public record RegisterUserQuery(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );

    public record RegisterTeacherQuery(
        string Email,
        string Username,
        string Password,
        string Firstname,
        string Lastname
    );
}
