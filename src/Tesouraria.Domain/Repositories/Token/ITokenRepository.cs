using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Repositories.Token;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshToken(string refreshToken, CancellationToken ct = default);
    Task AddRefreshToken(RefreshToken refreshToken); 
    Task<Entities.Globals.Tenant?> GetTenantByInviteCode(InviteCode inviteCode, CancellationToken ct = default);
    Task<InviteCode?> GetInviteCode(string code, CancellationToken ct = default);
    Task AddInviteCode(InviteCode inviteCode, CancellationToken ct = default);
}