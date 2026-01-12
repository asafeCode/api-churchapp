using FluentValidation;
using Tesouraria.Application.SharedValidators;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Application.UseCases.Commands.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator(IPasswordEncripter passwordEncripter, ILoggedUser loggedUser, IUserReadRepository repository)
    {
        RuleFor(cmd => cmd.Request.NewPassword).SetValidator(new PasswordValidator<ChangePasswordCommand>());
        RuleFor(x => x)
            .CustomAsync(async (command, context, ct) =>
            {
                var (userId, tenantId) = loggedUser.User();
                var user = await repository.GetActiveUserById(userId, tenantId, ct);
                
                if (passwordEncripter.IsValid(command.Request.Password, user!.PasswordHash).IsFalse())
                    context.AddFailure(ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD);
            });

    }
}