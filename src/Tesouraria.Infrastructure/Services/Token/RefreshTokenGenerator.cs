using Microsoft.Extensions.Options;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure.Services.Token;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly RefreshTokenSettings _settings;

    public RefreshTokenGenerator(IOptions<RefreshTokenSettings> settings)
    {
        _settings = settings.Value;
    }
    public RefreshToken CreateToken(Guid userId, Guid tenantId)
    {
        return new RefreshToken
        {
            Value = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            UserId = userId,
            TenantId = tenantId,
            ExpiresOn = DateTime.UtcNow.AddDays(_settings.ExpirationTimeDays)
        };
    } 
}