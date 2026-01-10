using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Repositories.Owner;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly TesourariaDbContext _dbContext;
    public OwnerRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> ExistOwnerWithId(Guid ownerId) => await _dbContext
        .Owner
        .AnyAsync(o => o.Id == ownerId);

    public async Task<Owner?> GetOwnerWithEmail(string email, CancellationToken ct = default) => await _dbContext
        .Owner
        .AsNoTracking()
        .FirstOrDefaultAsync(o => o.Email == email, ct);

    public async Task AddTenant(Tenant tenant, CancellationToken ct = default) => await _dbContext
        .Tenants.AddAsync(tenant, ct);
}