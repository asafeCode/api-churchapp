using FluentValidation;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Users.Create;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator(IUserReadRepository repository, ILoggedUser user)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("O papel do usuário é inválido.");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("A data de nascimento não pode ser no futuro.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        
        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellation) =>
            {
                var (_, tenantId) = user.User();
                if (await repository.ExistsUserWithName(command.Name.NormalizeUsername(), tenantId, cancellation))
                    context.AddFailure("Name", ResourceMessagesException.NAME_ALREADY_REGISTERED);
            });
    }
}