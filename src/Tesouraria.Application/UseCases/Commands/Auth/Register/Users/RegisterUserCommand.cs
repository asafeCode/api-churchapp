using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Users.Create;

public class RegisterUserCommand : ICommand
{
    public string Name { get; set; } =  string.Empty;
    public UserRole Role { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Password { get; set; } =   string.Empty;
}