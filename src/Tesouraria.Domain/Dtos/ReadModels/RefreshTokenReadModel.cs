using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.ReadModels;

public record RefreshTokenReadModel
{
    public DateTime ExpiresOn { get; init; }
    public Guid UserId { get; init; }
    
    public UserRole UserRole { get; init; }
    public Guid TenantId { get; init; }
};