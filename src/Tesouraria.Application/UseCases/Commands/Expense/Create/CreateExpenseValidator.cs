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

        
        When(cmd => cmd.Type == ExpenseType.Installment, () =>
        {
            RuleFor(cmd => cmd.TotalInstallments)
                .NotNull()
                .WithMessage("O total de parcelas é obrigatório para despesas parceladas.")
                .GreaterThan(1)
                .WithMessage("Despesas parceladas devem possuir mais de uma parcela.");

            RuleFor(cmd => cmd.AmountOfEachInstallment)
                .NotNull()
                .WithMessage("O valor da parcela é obrigatório para despesas parceladas.")
                .GreaterThan(0)
                .WithMessage("O valor da parcela deve ser maior que zero.");

            RuleFor(cmd => cmd.CurrentInstallment)
                .Must((cmd, currentInstallment) => 
                    !currentInstallment.HasValue || currentInstallment <= cmd.TotalInstallments)
                .WithMessage(cmd => 
                    $"A parcela atual ({cmd.CurrentInstallment}) não pode ser maior que o total de parcelas ({cmd.TotalInstallments}).");
            
            RuleFor(cmd => cmd.CurrentInstallment)
                .NotNull().GreaterThanOrEqualTo(0).WithMessage("A parcela atual deve ser maior ou igual a zero.");
        });

        When(cmd => cmd.Type != ExpenseType.Installment, () =>
        {
            RuleFor(cmd => cmd.CurrentInstallment)
                .Must(currentInstallment => !currentInstallment.HasValue)
                .WithMessage("Não pode informar parcela atual para despesas não parceladas.");

            RuleFor(cmd => cmd.TotalInstallments)
                .Must(totalInstallments => !totalInstallments.HasValue)
                .WithMessage("Não pode informar total de parcelas para despesas não parceladas.");

            RuleFor(cmd => cmd.AmountOfEachInstallment)
                .Must(amount => !amount.HasValue)
                .WithMessage("Não pode informar valor da parcela para despesas não parceladas.");
        });
    }
}