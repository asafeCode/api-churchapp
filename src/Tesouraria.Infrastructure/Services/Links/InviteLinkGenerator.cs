using Microsoft.Extensions.Options;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Services.Link;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure.Services.Links;

public class InviteLinkGenerator : IInviteLinkGenerator
{
    private readonly InviteSettings _settings;

    public InviteLinkGenerator(IOptions<InviteSettings> settings) => _settings = settings.Value;
    public string GetLink(InviteCode inviteCode)
    {
        return $"{_settings.FrontendUrl}/register/invite?Code={inviteCode.Value}";
    }
}