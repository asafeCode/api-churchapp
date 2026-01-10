using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure.Services.Token;

public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly JwtSettings _settings;
    public JwtTokenGenerator(IOptions<JwtSettings> options) => _settings = options.Value;

    public string Generate(JwtClaims claims)
    {
        var jwtClaims = new List<Claim>
        {
            new("TenantId", claims.TenantId.ToString()),
            new("UserId", claims.UserId.ToString()),
            new(ClaimTypes.Role, claims.UserRole),
        };
        
        return CreateToken(jwtClaims);
    }

    public string GenerateOwner(OwnerJwtClaims claims)
    {
        var jwtClaims = new List<Claim>
        {
            new("OwnerId", claims.OwnerId.ToString())
        };
        
        return CreateToken(jwtClaims);
    }
    private string CreateToken(List<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_settings.ExpirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_settings.SigningKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
    
}