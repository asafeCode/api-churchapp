using FluentValidation;

namespace Tesouraria.Application.UseCases.Commands.Worship.Create;

public class CreateWorshipValidator : AbstractValidator<CreateWorshipCommand>
{
    public CreateWorshipValidator()
    {
        RuleFor(cmd => cmd.DayOfWeek)
            .IsInEnum()
            .WithMessage("Dia da semana inválido.");

        RuleFor(cmd => cmd.Time)
            .Must(time => time != default)
            .WithMessage("Horário do culto é obrigatório.");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(255)
            .WithMessage("A descrição deve ter no máximo 255 caracteres.");
    }
}