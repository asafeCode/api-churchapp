using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Users;

public record ResponseUserProfileJson
{
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
public record ResponseUserProfileForDashboardJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
public record ResponseUsersJson
{
    public IEnumerable<ResponseUserProfileForDashboardJson> Users { get; set; } = [];
}