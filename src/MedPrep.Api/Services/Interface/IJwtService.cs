namespace MedPrep.Api.Services;

using System.Security.Claims;
using static MedPrep.Api.Config.JwtServiceContracts;

public interface IJwtService
{
    AccessTokenResult GenerateAccessToken(List<Claim> claims);
    RefreshTokenResult GenerateRefreshToken(List<Claim> claims);
    bool IsAccessTokenValid(string accessToken);
    bool IsRefreshTokenValid(string refreshToken);
}
