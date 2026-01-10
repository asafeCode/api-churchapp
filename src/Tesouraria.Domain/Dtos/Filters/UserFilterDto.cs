using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Filters;

public record UserFilterDto
{
    public string? Name { get; init; }
    public UserRole? Role { get; init; }
    public DateOnly? DateOfBirthInicial { get; init; }
    public DateOnly? DateOfBirthFinal { get; init; }
}
