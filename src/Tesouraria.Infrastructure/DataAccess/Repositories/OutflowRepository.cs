using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Repositories.Outflow;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class OutflowRepository : IOutflowRepository
{
    private readonly TesourariaDbContext _dbContext;
    public OutflowRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    
    public async Task Add(Outflow outflow, CancellationToken ct = default) => await _dbContext.Outflows.AddAsync(outflow, ct);
    public Task<Outflow?> GetForUpdate(Guid outflowId, Guid tenantId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Outflow?> GetById(Guid outflowId, Guid tenantId, CancellationToken ct = default) => await _dbContext
        .Outflows.AsNoTracking().SingleOrDefaultAsync(o => o.Id  == outflowId && o.Active && o.TenantId == tenantId, ct);
    
    public async Task<OutflowsDashboardReadModel> GetAll(OutflowFilterDto filter, Guid tenantId, CancellationToken ct = default)
    {
        var query = _dbContext.Outflows.AsNoTracking().Where(o => o.Active && o.TenantId == tenantId);

        if (filter.InitialDate.HasValue)
            query = query.Where(o => o.Date >= filter.InitialDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(o => o.Date <= filter.EndDate.Value);

        if (filter.PaymentMethod.HasValue)
            query = query.Where(o => o.PaymentMethod == filter.PaymentMethod.Value);
        
        if (filter.ExpenseType.HasValue)
            query = query.Where(o => o.Expense.Type == filter.ExpenseType.Value);

        if (filter.AmountMin.HasValue)
            query = query.Where(o => o.Amount >= filter.AmountMin.Value);

        if (filter.AmountMax.HasValue)
            query = query.Where(o => o.Amount <= filter.AmountMax.Value);

        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(o => o.Description != null && o.Description.Contains(filter.Description));
        
        if (filter.ExpenseId.HasValue)
            query = query.Where(o => o.ExpenseId == filter.ExpenseId.Value);

        if (filter.CreatedByUserId.HasValue)
            query = query.Where(o => o.CreatedByUserId == filter.CreatedByUserId.Value);
        
        var totalAmount = await query.SumAsync(o => o.Amount, ct);
        
        query = query.OrderByDescending(o => o.CreatedOn);
        
        var result = await query
            .Select(o => new OutflowDashboardReadModel(
                o.Id,
                o.Description ?? "—",
                o.Expense.Type,
                o.Expense.Name,
                o.Date,
                o.Amount,
                o.PaymentMethod,
                o.CurrentInstallmentPayed,
                o.Expense.TotalInstallments))
            .ToListAsync(ct);

        return new OutflowsDashboardReadModel(Outflows: result, TotalAmount: totalAmount);
    }
}