using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LocalHiringPlatform.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(
        IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier,
                user.EntityId.ToString()),

            new(ClaimTypes.Email,
                user.Email),

            new(ClaimTypes.Role,
                user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtSettings.Key));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.ExpiryMinutes),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}