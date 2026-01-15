using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Services.Token;

public interface IInviteCodeValidator
{
    public Task<InviteCodeReadModel> ValidateAndGetCode(string code, CancellationToken ct = default);
}