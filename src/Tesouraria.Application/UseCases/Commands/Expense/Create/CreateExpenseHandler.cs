using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Expenses;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Expense.Create;

public class CreateExpenseHandler : ICommandHandler<CreateExpenseCommand, ResponseRegisteredExpenseJson>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILoggedUser _loggedUser;

    public CreateExpenseHandler(
        IExpenseRepository writeRepository, 
        ILoggedUser loggedUser)
    {
        _expenseRepository = writeRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredExpenseJson> HandleAsync(CreateExpenseCommand command, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var expense = command.ToExpense(tenantId);

        await _expenseRepository.Add(expense, ct);
        return new ResponseRegisteredExpenseJson(expense.Id, expense.Name, expense.Type.ToString());
    }
}