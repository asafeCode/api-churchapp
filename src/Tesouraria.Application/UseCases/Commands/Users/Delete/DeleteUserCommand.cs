using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Users.Delete;

public record DeleteUserCommand(bool Force, Guid UserId) : ICommand{ }