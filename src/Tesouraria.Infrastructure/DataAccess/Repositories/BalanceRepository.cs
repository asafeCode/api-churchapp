using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Services.Balance;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class BalanceRepository : ICurrentBalance
{
    private readonly TesourariaDbContext _dbContext;
    public BalanceRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<BalanceReadModel> GetBalance(Guid tenantId, CancellationToken ct = default)
    {
        var totalInflowsAmount = await _dbContext.Inflows
            .AsNoTracking()
            .Where(i => i.Active && i.TenantId == tenantId)
            .SumAsync(i => i.Amount, ct);
        
        var totalOutflowsAmount = await _dbContext.Outflows
            .AsNoTracking()
            .Where(o => o.Active && o.TenantId == tenantId)
            .SumAsync(o => o.Amount, ct);

        return new BalanceReadModel
        {
            Balance = totalInflowsAmount - totalOutflowsAmount,
        };
    }
}