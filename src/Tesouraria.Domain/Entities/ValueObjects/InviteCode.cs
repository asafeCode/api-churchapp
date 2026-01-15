using Tesouraria.Domain.Entities.Globals;

namespace Tesouraria.Domain.Entities.ValueObjects;

public class InviteCode : EntityBase
{
    public DateTime ExpiresOn { get; init; }
    public string Value { get; init; } = string.Empty;
    public Tenant Tenant { get; init; } = null!;
}