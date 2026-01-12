using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Repositories.Expense;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly TesourariaDbContext _dbContext;
    public ExpenseRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    public async Task Add(Expense expense, CancellationToken ct = default) => await _dbContext
        .Expenses
        .AddAsync(expense, ct);

    public async Task<bool> ExistActiveExpenseWithId(Guid expenseId, Guid tenantId, CancellationToken ct = default) => await _dbContext
        .Expenses
        .AsNoTracking()
        .AnyAsync(expense => expense.Id == expenseId && expense.TenantId == tenantId, ct);
    
    public async Task<IEnumerable<Expense>> GetAll(Guid tenantId, CancellationToken ct = default) => await _dbContext
        .Expenses
        .AsNoTracking()
        .Where(exp => exp.Active && exp.TenantId == tenantId).OrderByDescending(exp => exp.CreatedOn).ToListAsync(ct);

    public async Task<Expense?> GetByIdWithTracking(Guid expenseId, Guid tenantId, CancellationToken ct = default) => await _dbContext.Expenses
        .FirstOrDefaultAsync(exp => exp.Id == expenseId && exp.TenantId == tenantId, ct);

    public async Task<Expense?> GetByIdWithoutTracking(Guid expenseId, Guid tenantId, CancellationToken ct = default) => await _dbContext.Expenses
        .AsNoTracking()
        .FirstOrDefaultAsync(exp => exp.Id == expenseId && exp.TenantId == tenantId, ct);
    
    public void Update(Expense expense) => _dbContext.Expenses
        .Update(expense);

    public Task Delete(Expense expense, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}