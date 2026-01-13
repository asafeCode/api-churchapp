using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Member;

public record RegisterMemberCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string InviteCode { get; set; } = string.Empty;
}