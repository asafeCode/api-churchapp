using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Services.Token;

public interface IAccessTokenGenerator
{
    public string Generate(JwtClaims claims);
    public string GenerateOwner(OwnerJwtClaims claims);
}
public record JwtClaims(Guid UserId, Guid TenantId, string UserRole);
public record OwnerJwtClaims(Guid OwnerId);