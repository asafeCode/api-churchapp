using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Expenses;
using Tesouraria.Domain.Repositories.Expense;

namespace Tesouraria.Application.UseCases.Queries.ExpenseDashboard;

public record GetExpenseDashboardQuery : IQuery;
public class ExpenseDashboardHandler : IHandler<GetExpenseDashboardQuery, ResponseExpensesJson>
{
    private readonly IExpenseRepository _repository;

    public ExpenseDashboardHandler(IExpenseRepository repository) =>  _repository = repository; 
    public async Task<ResponseExpensesJson> HandleAsync(GetExpenseDashboardQuery query, CancellationToken ct = default)
    {
        var worships = await _repository.GetAll(ct);

        var response = worships.Select(expense => new ResponseExpenseJson
        {
            Id = expense.Id,
            Name = expense.Name,
            ExpenseType = expense.Type,
            TotalInstallments = expense.TotalInstallments,
        });

        return new ResponseExpensesJson
        {
            Expenses = response
        };
    }
}