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

    public async Task<bool> ExistActiveExpenseWithId(Guid? expenseId, CancellationToken ct = default) => await _dbContext
        .Expenses
        .AsNoTracking()
        .AnyAsync(expense => expense.Id == expenseId, ct);
    
    public async Task<IEnumerable<Expense>> GetAll(CancellationToken ct = default) => await _dbContext
        .Expenses
        .AsNoTracking()
        .Where(exp => exp.Active).ToListAsync(ct);

    public async Task<Expense?> GetById(Guid expenseId, CancellationToken ct = default) => await _dbContext.Expenses
        .AsNoTracking()
        .FirstOrDefaultAsync(exp => exp.Id == expenseId, ct);
    
    public void Update(Expense expense) => _dbContext.Expenses
        .Update(expense);

    public Task Delete(Expense expense, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}