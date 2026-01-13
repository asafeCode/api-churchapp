using FluentValidation;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.User.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator(
        IUserReadRepository repository,
        ILoggedUser loggedUser)
    {
        // Obrigatórios
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("A data de nascimento não pode ser no futuro.");

        // Opcionais (mantive tudo igual)
        RuleFor(x => x.FullName)
            .MaximumLength(150)
            .When(x => x.FullName.IsNotEmpty());

        RuleFor(x => x.Gender)
            .IsInEnum()
            .When(x => x.Gender.HasValue);

        RuleFor(x => x.Phone)
            .MaximumLength(20)
            .When(x => x.Phone.IsNotEmpty());

        RuleFor(x => x)
            .CustomAsync(async (command, context, ct) =>
            {
                var (userId, tenantId) = loggedUser.User();

                var currentUser =
                    await repository.GetActiveUserById(userId, tenantId, ct);

                var normalizedNewName =
                    command.Username.NormalizeUsername();

                if (normalizedNewName == currentUser!.NormalizedUsername)
                    return;

                if (await repository.ExistsUserWithName(
                        normalizedNewName,
                        tenantId,
                        ct))
                {
                    context.AddFailure(
                        nameof(command.Username),
                        ResourceMessagesException.NAME_ALREADY_REGISTERED);
                }
            });
    }
}