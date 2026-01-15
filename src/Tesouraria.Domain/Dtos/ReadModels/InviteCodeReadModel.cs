namespace Tesouraria.Domain.Dtos.ReadModels;

public record InviteCodeReadModel
{
    public Guid TenantId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public string Value { get; init; } =  string.Empty;
    public DateTime ExpiresOn { get; init; }
};