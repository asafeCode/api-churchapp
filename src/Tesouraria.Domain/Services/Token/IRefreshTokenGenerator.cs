using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Services.Token;

public interface IRefreshTokenGenerator
{
    public RefreshToken CreateToken(Guid userId, Guid tenantId);
}