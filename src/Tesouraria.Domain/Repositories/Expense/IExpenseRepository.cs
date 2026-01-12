namespace Tesouraria.Domain.Repositories.Expense;

public interface IExpenseRepository
{
    Task Add(Entities.ValueObjects.Expense expense, CancellationToken ct = default);
    
    Task<IEnumerable<Entities.ValueObjects.Expense>> GetAll(Guid tenantId, CancellationToken ct = default);
    Task<Entities.ValueObjects.Expense?> GetByIdWithoutTracking(Guid expenseId, Guid tenantId ,CancellationToken ct = default);
    Task<Entities.ValueObjects.Expense?> GetByIdWithTracking(Guid expenseId, Guid tenantId ,CancellationToken ct = default);
    
    void Update(Entities.ValueObjects.Expense expense);
    
    // Não é possível deletar a despesa, se existir saídas associadas.
    Task Delete(Entities.ValueObjects.Expense expense, CancellationToken ct = default);
}