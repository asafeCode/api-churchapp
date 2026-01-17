using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Repositories.Tenant;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly TesourariaDbContext _context;
    public TenantRepository(TesourariaDbContext context) =>  _context = context;

    public async Task<IEnumerable<Tenant>> GetAll(CancellationToken ct = default) => await _context
        .Tenants
        .AsNoTracking()
        .Where(t => t.Active)
        .ToListAsync(ct);
    
    public async Task<bool> ExistTenantWithId(Guid tenantId, CancellationToken ct = default) => await _context
        .Tenants
        .AnyAsync(t => t.Id == tenantId && t.Active, ct);
}