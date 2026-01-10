namespace Tesouraria.Infrastructure.Settings;

public sealed record VerificationTokenSettings
{
    public int ExpirationTimeMinutes { get; init; }
}