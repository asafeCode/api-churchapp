using FluentValidation;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Expense.Create;

public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("O nome da despesa é obrigatório.")
            .MaximumLength(150)
            .WithMessage("O nome da despesa deve ter no máximo 150 caracteres.");

        RuleFor(cmd => cmd.Type)
            .IsInEnum()
            .WithMessage("Tipo de despesa inválido.");

        RuleFor(cmd => cmd.TotalInstallments)
            .GreaterThan(1)
            .When(cmd => cmd.TotalInstallments.HasValue)
            .WithMessage("O total de parcelas deve ser maior que 1.");

        RuleFor(cmd => cmd)
            .Must(cmd =>
                cmd.Type == ExpenseType.Installment || !cmd.TotalInstallments.HasValue)
            .WithMessage("Parcelas não devem ser informadas para despesas que não são parceladas.");

        RuleFor(cmd => cmd)
            .Must(cmd =>
                cmd.Type != ExpenseType.Installment || cmd.TotalInstallments.HasValue)
            .WithMessage("Despesas parceladas devem possuir o total de parcelas.");
    }
}