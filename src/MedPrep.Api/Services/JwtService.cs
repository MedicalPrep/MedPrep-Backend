namespace MedPrep.Api.Services;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedPrep.Api.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService(IOptions<AuthTokenConfig> config) : IJwtService
{
    private readonly AuthTokenConfig config = config.Value;
    private readonly JwtSecurityTokenHandler tokenHandler = new();

    public JwtServiceContracts.AccessTokenResult GenerateAccessToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.config.AccessTokenSecret)
        );

        var accessToken = new JwtSecurityToken(
            issuer: this.config.Issuer,
            audience: this.config.Audience,
            expires: DateTime.Now.AddHours(this.config.AccessTokenExpiration),
            claims: claims,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new(this.tokenHandler.WriteToken(accessToken), accessToken.ValidTo);
    }

    public JwtServiceContracts.RefreshTokenResult GenerateRefreshToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.config.RefreshTokenSecret)
        );

        var refreshToken = new JwtSecurityToken(
            issuer: this.config.Issuer,
            audience: this.config.Audience,
            expires: DateTime.Now.AddHours(this.config.RefreshTokenExpiration),
            claims: claims,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new(this.tokenHandler.WriteToken(refreshToken), refreshToken.ValidTo);
    }

    public bool IsAccessTokenValid(string accessToken) => throw new NotImplementedException();

    public bool IsRefreshTokenValid(string refreshToken) => throw new NotImplementedException();
}
