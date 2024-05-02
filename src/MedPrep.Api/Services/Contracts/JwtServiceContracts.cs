namespace MedPrep.Api.Config;

public static class JwtServiceContracts
{
    public record AccessTokenResult(string AccessToken, DateTime AccessTokenExpiration);

    public record RefreshTokenResult(string RefreshToken, DateTime RefreshTokenExpiration);
}
