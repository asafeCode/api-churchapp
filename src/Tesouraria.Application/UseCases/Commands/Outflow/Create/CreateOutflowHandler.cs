using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Repositories.Outflow;
using Tesouraria.Domain.Services.Balance;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Outflow.Create;

public class CreateOutflowHandler : ICommandHandler<CreateOutflowCommand, ResponseCreatedOutflowJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IOutflowRepository _outflowRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICurrentBalance _currentBalance;

    public CreateOutflowHandler(
        ILoggedUser loggedUser, 
        IOutflowRepository outflowRepository, 
        IExpenseRepository expenseRepository, 
        ICurrentBalance currentBalance
        )
    {
        _loggedUser = loggedUser;
        _outflowRepository = outflowRepository;
        _expenseRepository = expenseRepository;
        _currentBalance = currentBalance;
    }
    public async Task<ResponseCreatedOutflowJson> HandleAsync(CreateOutflowCommand command, CancellationToken ct = default)
    {
        var (loggedUserId, tenantId) =  _loggedUser.User();
        var expense = await _expenseRepository.GetByIdWithTracking(command.ExpenseId, tenantId, ct);
        var currentBalance = await _currentBalance.GetBalance(tenantId, ct);
        if ( expense is null ) throw new NotFoundException("Despesa não encontrada.");
        
        if ( expense is 
             { 
                 CurrentInstallment: not null, 
                 TotalInstallments: not null 
             } &&
             expense.CurrentInstallment.Value >= expense.TotalInstallments.Value 
             ) throw new ValidationException("Todas as parcelas já foram pagas");

        if ( expense.Type == ExpenseType.Installment )
        {
            expense.CurrentInstallment += 1;
        }
        var outflow = command.ToOutflow(loggedUserId, tenantId, expense);


        if (outflow.Amount > currentBalance.Balance)
            throw new ValidationException("Saldo insuficiente.");
        
        await _outflowRepository.Add(outflow, ct);
        

        return new ResponseCreatedOutflowJson(outflow.Id, outflow.Amount);
    }
}