using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.Inflow;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class InflowRepository : IInflowRepository
{
    private readonly TesourariaDbContext _dbContext;
    public InflowRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    public async Task Add(Inflow inflow, CancellationToken cancellationToken = default) => await _dbContext
        .Inflows.AddAsync(inflow, cancellationToken);

    public Task<Inflow?> GetForUpdate(Guid inflowId, Guid tenantId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Inflow?> GetById(Guid inflowId, Guid tenantId, CancellationToken ct = default) => await _dbContext
        .Inflows
        .AsNoTracking()
        .SingleOrDefaultAsync(inf => inf.Id == inflowId && inf.TenantId == tenantId , ct);
    
    public async Task<InflowsDashboardReadModel> GetAll(InflowFilterDto filter, Guid tenantId,  CancellationToken ct = default)
    {
        var query = _dbContext.Inflows.AsNoTracking().Where(i => i.Active &&  i.TenantId == tenantId);

        if (filter.InitialDate.HasValue)
            query = query.Where(i => i.Date >= filter.InitialDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(i => i.Date <= filter.EndDate.Value);

        if (filter.Type.HasValue)
            query = query.Where(i => i.Type == filter.Type.Value);

        if (filter.PaymentMethod.HasValue)
            query = query.Where(i => i.PaymentMethod == filter.PaymentMethod.Value);

        if (filter.AmountMin.HasValue)
            query = query.Where(i => i.Amount >= filter.AmountMin.Value);

        if (filter.AmountMax.HasValue)
            query = query.Where(i => i.Amount <= filter.AmountMax.Value);

        if (filter.Description.IsNotEmpty())
            query = query.Where(i => i.Description != null && i.Description.Contains(filter.Description));

        if (filter.MemberId.HasValue)
            query = query.Where(i => i.MemberId == filter.MemberId.Value);       
        
        if (filter.CreatedByUserId.HasValue)
            query = query.Where(i => i.CreatedByUserId == filter.CreatedByUserId.Value);

        if (filter.WorshipId.HasValue)
            query = query.Where(i => i.WorshipId == filter.WorshipId.Value);

        query = query.OrderByDescending(o => o.CreatedOn);

        var orderBy = filter.OrderBy;
        var asc = filter.OrderDirection ?? OrderDirection.Asc;

        query = orderBy switch
        {
            InflowOrderBy.Amount => asc == OrderDirection.Asc
                ? query.OrderBy(i => i.Amount)
                : query.OrderByDescending(i => i.Amount),
            
            InflowOrderBy.Date => asc == OrderDirection.Asc
                ? query.OrderBy(i => i.Date)
                : query.OrderByDescending(i => i.Date),
            _ => query
        };
        
        var totalAmount = await query.SumAsync(i => i.Amount, ct);

        var result = await query
            .Select(i => new InflowDashboardReadModel(
                i.Id,
                i.Description ?? "—",
                i.Date,
                i.Type,
                i.Member != null ? i.Member.Username : "—",
                i.Worship != null ? i.Worship.DayOfWeek : null,
                i.Worship != null ? i.Worship.Time : null,
                i.PaymentMethod,
                i.Amount
            ))
            .ToListAsync(ct);
        
        return new InflowsDashboardReadModel(result, totalAmount);
    }
    
}