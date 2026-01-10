namespace Tesouraria.Domain.Entities.ValueObjects;

public class RefreshToken : EntityBase
{
    public DateTime ExpiresOn { get; init; }
    public required string Value { get; init; } = string.Empty;
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
}