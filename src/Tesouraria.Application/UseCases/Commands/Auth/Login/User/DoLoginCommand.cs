using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Auth.Login.User;

public class DoLoginCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
}