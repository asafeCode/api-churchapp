using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Users;

public record RegisterUserCommand : ICommand
{
    public string Name { get; init; } =  string.Empty;
    public UserRole Role { get; init; }
    public static DateOnly DateOfBirth  => DateOnly.MinValue;
    public string Password => $"{Name}123";
}