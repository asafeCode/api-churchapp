using FluentValidation;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Repositories.Worship;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Inflow.Create;

public class CreateInflowValidator : AbstractValidator<CreateInflowCommand>
{
    public CreateInflowValidator(IWorshipRepository worshipRepository, IUserReadRepository userRepository, ILoggedUser user)
    {
        var loggedUser = user.User();
        RuleFor(cmd => cmd.Date)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("A data não pode ser no futuro.");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");

        RuleFor(cmd => cmd.Type)
            .IsInEnum().WithMessage("Tipo de entrada inválido.");

        RuleFor(cmd => cmd.PaymentMethod)
            .IsInEnum().WithMessage("Método de pagamento inválido.");

        RuleFor(cmd => cmd.Amount)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

        RuleFor(cmd => cmd.WorshipId)
            .Must(id => id is null || id != Guid.Empty)
            .WithMessage("O culto selecionado é inválido.")
            .MustAsync(async (worshipId, ct) =>
            {
                if (!worshipId.HasValue)
                    return true;

                return await worshipRepository
                    .ExistActiveWorshipWithId(worshipId.Value, loggedUser.tenantId, ct);
            })
            .When(cmd => cmd.WorshipId.HasValue && cmd.WorshipId != Guid.Empty)
            .WithMessage("Culto não encontrado.");

        RuleFor(cmd => cmd.UserId)
            .Must(id => id is null || id != Guid.Empty)
            .WithMessage("O usuário selecionado é inválido.")

            .MustAsync(async (userId, ct) =>
            {
                if (!userId.HasValue)
                    return true;

                return await userRepository
                    .ExistActiveUserWithId(userId.Value, loggedUser.tenantId, ct);
            })
            .When(cmd => cmd.UserId.HasValue && cmd.UserId != Guid.Empty)
            .WithMessage("Usuário não encontrado.");
    }
}
