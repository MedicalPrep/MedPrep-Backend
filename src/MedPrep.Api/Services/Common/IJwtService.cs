namespace MedPrep.Api.Services.Common;

using System.Security.Claims;
using static MedPrep.Api.Services.Contracts.JwtServiceContracts;

public interface IJwtService
{
    AccessTokenResult GenerateAccessToken(ICollection<Claim> claims);
    RefreshTokenResult GenerateRefreshToken(ICollection<Claim> claims);
    Task<bool> IsAccessTokenValid(string accessToken);
    Task<bool> IsRefreshTokenValid(string refreshToken);
    Task<IEnumerable<Claim>?> DecodeAccessTokenClaims(string accessToken);
    Task<IEnumerable<Claim>?> DecodeRefreshTokenClaims(string refreshToken);
}
