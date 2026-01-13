using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Services.Link;

public interface IInviteLinkGenerator
{
    public string GetLink(InviteCode inviteCode);
}