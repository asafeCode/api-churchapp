using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Tenants.Create;

public record CreateTenantCommand(string Name, string? DomainName) : ICommand;