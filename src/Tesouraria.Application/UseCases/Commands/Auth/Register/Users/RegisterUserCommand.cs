using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Users;

public class RegisterUserCommand : ICommand
{
    public string Name { get; set; } =  string.Empty;
    public UserRole Role { get; set; }
    public static DateOnly DateOfBirth  => DateOnly.MinValue;
    public string Password => $"{Name}123";
}