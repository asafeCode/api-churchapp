using FluentValidation;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Users;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator(IUserReadRepository repository, ILoggedUser user)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("O papel do usuário é inválido.");
        
        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellation) =>
            {
                var (_, tenantId) = user.User();
                if (await repository.ExistsUserWithName(command.Name.NormalizeUsername(), tenantId, cancellation))
                    context.AddFailure("Name", ResourceMessagesException.NAME_ALREADY_REGISTERED);
            });
    }
}