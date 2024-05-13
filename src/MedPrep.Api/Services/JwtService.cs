namespace MedPrep.Api.Services;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedPrep.Api.Config;
using MedPrep.Api.Services.Common;
using MedPrep.Api.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService(IOptions<AuthTokenConfig> config) : IJwtService
{
    private readonly AuthTokenConfig config = config.Value;
    private readonly JwtSecurityTokenHandler tokenHandler = new();

    public JwtServiceContracts.AccessTokenResult GenerateAccessToken(ICollection<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.config.AccessTokenSecret)
        );

        var expiration = DateTimeOffset.UtcNow.AddHours(this.config.RefreshTokenExpiration);
        var accessToken = new JwtSecurityToken(
            issuer: this.config.Issuer,
            audience: this.config.Audience,
            expires: expiration.DateTime,
            claims: claims,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new(this.tokenHandler.WriteToken(accessToken), expiration);
    }

    public JwtServiceContracts.RefreshTokenResult GenerateRefreshToken(ICollection<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.config.RefreshTokenSecret)
        );

        var expiration = DateTimeOffset.UtcNow.AddHours(this.config.RefreshTokenExpiration);
        var refreshToken = new JwtSecurityToken(
            issuer: this.config.Issuer,
            audience: this.config.Audience,
            expires: expiration.DateTime,
            claims: claims,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new(this.tokenHandler.WriteToken(refreshToken), expiration);
    }

    public async Task<bool> IsAccessTokenValid(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = false,
            ValidIssuer = this.config.Issuer,
            ValidAudience = this.config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.config.AccessTokenSecret)
            ),
        };
        var result = await this.tokenHandler.ValidateTokenAsync(
            accessToken,
            tokenValidationParameters
        );

        return result.IsValid;
    }

    public async Task<bool> IsRefreshTokenValid(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = false,
            ValidIssuer = this.config.Issuer,
            ValidAudience = this.config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.config.RefreshTokenSecret)
            ),
        };
        var result = await this.tokenHandler.ValidateTokenAsync(
            refreshToken,
            tokenValidationParameters
        );

        return result.IsValid;
    }

    public async Task<IEnumerable<Claim>?> DecodeAccessTokenClaims(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = false,
            ValidIssuer = this.config.Issuer,
            ValidAudience = this.config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.config.AccessTokenSecret)
            ),
        };
        var result = await this.tokenHandler.ValidateTokenAsync(
            accessToken,
            tokenValidationParameters
        );

        if (result.Exception is null)
        {
            return null;
        }

        return result.ClaimsIdentity.Claims;
    }

    public async Task<IEnumerable<Claim>?> DecodeRefreshTokenClaims(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = false,
            ValidIssuer = this.config.Issuer,
            ValidAudience = this.config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.config.RefreshTokenSecret)
            ),
        };
        var result = await this.tokenHandler.ValidateTokenAsync(
            refreshToken,
            tokenValidationParameters
        );

        if (result.Exception is not null)
        {
            return null;
        }

        return result.ClaimsIdentity.Claims;
    }
}
