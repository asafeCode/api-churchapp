using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Services.Token;

public interface IInviteCodeValidator
{
    public Task<InviteCode> ValidateAndGetCode(string code, CancellationToken ct = default);
}