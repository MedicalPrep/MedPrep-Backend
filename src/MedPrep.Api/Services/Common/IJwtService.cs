namespace MedPrep.Api.Services.Common;

using System.Security.Claims;
using static MedPrep.Api.Services.Contracts.JwtServiceContracts;

public interface IJwtService
{
    AccessTokenResult GenerateAccessToken(List<Claim> claims);
    RefreshTokenResult GenerateRefreshToken(List<Claim> claims);
    bool IsAccessTokenValid(string accessToken);
    bool IsRefreshTokenValid(string refreshToken);
}
