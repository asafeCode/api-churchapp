using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Services.Token;

public interface IInviteCodeGenerator
{
    InviteCode CreateCode(Guid tenantId);
}