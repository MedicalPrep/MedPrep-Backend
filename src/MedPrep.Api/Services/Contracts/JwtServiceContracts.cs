namespace MedPrep.Api.Services.Contracts;

public static class JwtServiceContracts
{
    public record AccessTokenResult(string AccessToken, DateTime AccessTokenExpiration);

    public record RefreshTokenResult(string RefreshToken, DateTime RefreshTokenExpiration);
}
