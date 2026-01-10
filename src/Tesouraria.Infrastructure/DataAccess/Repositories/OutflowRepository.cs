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

    public async Task<Outflow?> GetById(Guid outflowId, CancellationToken ct = default) => await _dbContext
        .Outflows.AsNoTracking().SingleOrDefaultAsync(o => o.Id  == outflowId && o.Active, ct);
    
    public async Task<IEnumerable<OutflowDashboardReadModel>> GetAll(OutflowFilterDto filter, CancellationToken ct = default)
    {
        var query = _dbContext.Outflows.AsNoTracking().Where(o => o.Active);

        if (filter.InitialDate.HasValue)
            query = query.Where(o => o.Date >= filter.InitialDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(o => o.Date <= filter.EndDate.Value);

        if (filter.PaymentMethod.HasValue)
            query = query.Where(o => o.PaymentMethod == filter.PaymentMethod.Value);

        if (filter.AmountMin.HasValue)
            query = query.Where(o => o.Amount >= filter.AmountMin.Value);

        if (filter.AmountMax.HasValue)
            query = query.Where(o => o.Amount <= filter.AmountMax.Value);

        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(o => o.Description != null && o.Description.Contains(filter.Description));

        if (filter.CurrentInstallment.HasValue)
            query = query.Where(o => o.CurrentInstallment == filter.CurrentInstallment.Value);

        if (filter.ExpenseId.HasValue)
            query = query.Where(o => o.ExpenseId == filter.ExpenseId.Value);

        if (filter.CreatedByUserId.HasValue)
            query = query.Where(o => o.CreatedByUserId == filter.CreatedByUserId.Value);

        return await query
            .Select(o => new OutflowDashboardReadModel(
                o.Id,
                o.Expense.Name,
                o.Date,
                o.Amount,
                o.PaymentMethod,
                o.CurrentInstallment,
                o.Expense.TotalInstallments))
            .ToListAsync(ct);
    }
    
    public void Update(Outflow outflow) => _dbContext.Outflows.Update(outflow);

    public Task Delete(Outflow outflow, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}