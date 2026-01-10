using Microsoft.EntityFrameworkCore.Storage;
using Tesouraria.Domain.Repositories;

namespace Tesouraria.Infrastructure.DataAccess;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TesourariaDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(TesourariaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginAsync(CancellationToken ct = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
        await _transaction!.CommitAsync(ct);
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(ct);
    }
}