using FluentValidation;
using Tesouraria.Application.SharedValidators;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Member;

public class RegisterMemberValidator : AbstractValidator<RegisterMemberCommand>
{
    public RegisterMemberValidator(ILoggedUser user, IUserReadRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        
        RuleFor(cmd => cmd.Password).SetValidator(new PasswordValidator<RegisterMemberCommand>());
        RuleFor(cmd => cmd.InviteCode)
            .NotEmpty().WithMessage("Código de convite não pode ser vazio.")
            .Must(cmd => Guid.TryParse(cmd, out _))
            .WithMessage("Código de convite inválido.");
        
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                var (_, tenantId) = user.User();
                return !await repository.ExistsUserWithName(name.NormalizeUsername(), tenantId, cancellation);
            })
            .WithMessage(ResourceMessagesException.NAME_ALREADY_REGISTERED);
    }
}