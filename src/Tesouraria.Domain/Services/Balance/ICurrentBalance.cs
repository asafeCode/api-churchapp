using Tesouraria.Domain.Dtos.ReadModels;

namespace Tesouraria.Domain.Services.Balance;

public interface ICurrentBalance
{
    Task<BalanceReadModel> GetBalance(Guid tenantId, CancellationToken ct = default);
}