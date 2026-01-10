using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.User.ChangePassword;

public record ChangePasswordCommand(ChangePasswordRequest Request) : ICommand;

public record ChangePasswordRequest(string Password, string NewPassword);