using FluentValidation;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Repositories.User;

namespace Tesouraria.Application.UseCases.Commands.Outflow.Create;

public class CreateOutflowValidator : AbstractValidator<CreateOutflowCommand>
{
    public CreateOutflowValidator(IExpenseRepository expenseRepository)
    {
        RuleFor(cmd => cmd.Date)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("A data não pode ser no futuro.");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");

        RuleFor(cmd => cmd.PaymentMethod)
            .IsInEnum().WithMessage("Método de pagamento inválido.");

        RuleFor(cmd => cmd.Amount)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
        
        RuleFor(cmd => cmd)
            .CustomAsync(async (cmd, context, ct) =>
            {
                if (cmd.ExpenseId == Guid.Empty)
                {
                    context.AddFailure("ExpenseId", "A despesa selecionada é inválida.");
                    return;
                }
                
                var expense = await expenseRepository.GetById(cmd.ExpenseId, ct);
                
                if (expense is null)
                {
                    context.AddFailure("ExpenseId", "Despesa não encontrada.");
                    return;
                }

                if (expense.Type == ExpenseType.Installment &&
                    cmd.CurrentInstallment is null)
                {
                    context.AddFailure("CurrentInstallment",
                        "Despesas parceladas exigem o número da parcela.");
                }

                if (expense.Type == ExpenseType.Installment &&
                    cmd.CurrentInstallment is { } current &&
                    (current <= 0 || current > expense.TotalInstallments))
                {
                    context.AddFailure("CurrentInstallment",
                        $"A parcela deve estar entre 1 e {expense.TotalInstallments}.");
                }

                if (expense.Type != ExpenseType.Installment &&
                    cmd.CurrentInstallment is not null)
                {
                    context.AddFailure("CurrentInstallment",
                        "Parcelas não devem ser informadas para despesas não parceladas.");
                }
            });

    }
}
