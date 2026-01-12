using System.Security.Cryptography;
using FluentValidation;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Services.Balance;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Outflow.Create;

public class CreateOutflowValidator : AbstractValidator<CreateOutflowCommand>
{
    public CreateOutflowValidator(IExpenseRepository expenseRepository, ILoggedUser loggedUser)
    {
        var (_, tenantId) = loggedUser.User();
        RuleFor(cmd => cmd.Date)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("A data não pode ser no futuro.");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");

        RuleFor(cmd => cmd.PaymentMethod)
            .IsInEnum().WithMessage("Método de pagamento inválido.");

        RuleFor(cmd => cmd)
            .CustomAsync( async (command, context, ct) =>
            {
                var expense = await expenseRepository.GetByIdWithoutTracking(command.ExpenseId, tenantId, ct);
                if (expense is null)
                {
                    context.AddFailure("Despesa inválida");
                    return;
                }

                if (expense.Type == ExpenseType.Installment && command.Amount.HasValue)
                {
                    context.AddFailure("Esta despesa é parcelada. O valor da saída corresponde à parcela e não pode ser alterado aqui.");
                    return;
                }
                
                if (command.Amount is <= 0) context.AddFailure("O valor deve ser informado.");
            });
    }
}
