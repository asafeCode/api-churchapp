using Microsoft.Extensions.Options;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure.Services.Token;

public class InviteCodeGenerator : IInviteCodeGenerator
{
    private readonly InviteSettings _settings;

    public InviteCodeGenerator(IOptions<InviteSettings> settings) => _settings = settings.Value;
    
    public InviteCode CreateCode(Guid tenantId)
    {
        return new InviteCode
        {
            Value = $"{Guid.NewGuid()}",
            TenantId = tenantId,
            ExpiresOn = DateTime.UtcNow.AddMinutes(_settings.ExpirationTimeMinutes)
        };
    } 
}