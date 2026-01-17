namespace Tesouraria.Domain.Repositories.Expense;

public interface IExpenseRepository
{
    Task Add(Entities.ValueObjects.Expense expense, CancellationToken ct = default);
    
    Task<IEnumerable<Entities.ValueObjects.Expense>> GetAll(Guid tenantId, CancellationToken ct = default);
    Task<Entities.ValueObjects.Expense?> GetByIdWithoutTracking(Guid expenseId, Guid tenantId ,CancellationToken ct = default);
    Task<Entities.ValueObjects.Expense?> GetByIdWithTracking(Guid expenseId, Guid tenantId ,CancellationToken ct = default);
}