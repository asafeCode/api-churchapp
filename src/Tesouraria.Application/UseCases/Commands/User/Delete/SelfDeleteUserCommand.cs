using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.User.Delete;

public record SelfDeleteUserCommand(bool Force) : ICommand;