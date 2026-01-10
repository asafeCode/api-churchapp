using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Token.RefreshToken;
public class CreateNewTokenCommand : ICommand
{
    public string RefreshToken { get; set; } = string.Empty;
}