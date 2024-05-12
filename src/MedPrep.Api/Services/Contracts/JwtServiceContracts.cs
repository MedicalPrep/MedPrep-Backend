namespace MedPrep.Api.Services.Contracts;

public static class JwtServiceContracts
{
    public record AccessTokenResult(string AccessToken, DateTimeOffset AccessTokenExpiration);

    public record RefreshTokenResult(string RefreshToken, DateTimeOffset RefreshTokenExpiration);
}
