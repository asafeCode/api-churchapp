namespace Tesouraria.Infrastructure.Settings;

public sealed record InviteSettings
{
    public int ExpirationTimeMinutes { get; init; }
    public string FrontendUrl { get; set; } = string.Empty;
}