using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Auth.Login.Owner;

public record OwnerLoginCommand(string Email, string Password) : ICommand;