using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Auth.OwnerLogin;

public record OwnerLoginCommand(string Email, string Password) : ICommand;