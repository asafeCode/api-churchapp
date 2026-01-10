using FluentValidation;
using Tesouraria.Application.SharedValidators;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Application.UseCases.Commands.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator(IPasswordEncripter passwordEncripter, ILoggedUser loggedUser, IUserReadRepository repository)
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
        RuleFor(x => x)
            .CustomAsync(async (command, context, ct) =>
            {
                var (userId, tenantId) = loggedUser.User();
                var user = await repository.GetActiveUserById(userId, tenantId, ct);

                if (!passwordEncripter.IsValid(command.Password, user!.PasswordHash))
                    context.AddFailure(ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD);
            });

    }
}