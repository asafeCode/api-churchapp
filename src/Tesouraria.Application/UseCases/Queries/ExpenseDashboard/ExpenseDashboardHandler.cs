using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Expenses;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.ExpenseDashboard;

public record GetExpenseDashboardQuery : IQuery;
public class ExpenseDashboardHandler : IHandler<GetExpenseDashboardQuery, ResponseExpensesJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IExpenseRepository _repository;

    public ExpenseDashboardHandler(IExpenseRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseExpensesJson> HandleAsync(GetExpenseDashboardQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var expenses = await _repository.GetAll(tenantId, ct);

        var response = expenses.Select(expense => new ResponseExpenseJson
        {
            Id = expense.Id,
            Name = expense.Name,
            ExpenseType = expense.Type,
            TotalInstallments = expense.TotalInstallments,
            CurrentInstallment = expense.CurrentInstallment,
            AmountOfEachInstallment = expense.AmountOfEachInstallment,
        });

        return new ResponseExpensesJson
        {
            Expenses = response
        };
    }
}