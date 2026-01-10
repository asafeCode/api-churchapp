using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure.Services.Token;

public sealed class JwtTokenValidator : IAccessTokenValidator
{
    private readonly JwtSettings _settings;

    public JwtTokenValidator(IOptions<JwtSettings> settings) => _settings = settings.Value;

    public JwtClaims ValidateUserToken(string token)
    {
        var principal = ValidateToken(token);

        var userId = ParseGuid(principal, "UserId");
        var tenantId = ParseGuid(principal, "TenantId");
        var role = RoleToString(principal);

        return new JwtClaims(userId, tenantId, role);
    }

    public OwnerJwtClaims ValidateOwnerToken(string token)
    {
        var principal = ValidateToken(token);

        var ownerId = ParseGuid(principal, "OwnerId");

        return new OwnerJwtClaims(ownerId);
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var validationParams = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SigningKey)),
            ClockSkew = TimeSpan.Zero
        };

        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, validationParams, out var validatedToken);

        if (validatedToken is not JwtSecurityToken jwt || 
            !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            throw new SecurityTokenException("Token inválido");

        return principal;
    }

    private static Guid ParseGuid(ClaimsPrincipal principal, string claimType)
    {
        var value = principal.Claims.FirstOrDefault(c => c.Type == claimType)?.Value
                    ?? throw new SecurityTokenException($"Claim {claimType} ausente");

        return !Guid.TryParse(value, out var guid) ? 
            throw new SecurityTokenException($"Claim {claimType} inválida") 
            : 
            guid;
    }

    private static string RoleToString(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                      ?? throw new SecurityTokenException("Claim Role ausente");
    }
}