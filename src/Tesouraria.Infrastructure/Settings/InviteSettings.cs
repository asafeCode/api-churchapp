namespace Tesouraria.Infrastructure.Settings;

public sealed record InviteCodeSettings
{
    public int ExpirationTimeMinutes { get; init; }
}