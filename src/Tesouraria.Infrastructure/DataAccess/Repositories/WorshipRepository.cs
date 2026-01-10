using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Repositories.Worship;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class WorshipRepository : IWorshipRepository
{
    private readonly TesourariaDbContext _dbContext;

    public WorshipRepository(TesourariaDbContext dbContext)
        => _dbContext = dbContext;

    public async Task Add(Worship worship, CancellationToken ct = default) => await _dbContext
        .Worships
        .AddAsync(worship, ct);
    
    public async Task<bool> ExistActiveWorshipWithId(Guid? worshipId, CancellationToken ct = default) => await _dbContext
        .Worships
        .AsNoTracking()
        .AnyAsync(w => w.Id == worshipId && w.Active, ct);
    
    public async Task<IEnumerable<Worship>> GetAll(CancellationToken ct = default) => await _dbContext.Worships
            .AsNoTracking()
            .Where(w => w.Active)
            .ToListAsync(ct);

    public void Update(Worship worship, CancellationToken ct = default) => _dbContext
        .Worships
        .Update(worship);
    
    public Task Delete(Worship worship, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}