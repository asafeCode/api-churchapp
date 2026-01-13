using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Repositories.Token;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public sealed class TokenRepository :  ITokenRepository
{
    private readonly TesourariaDbContext _dbContext;
    public TokenRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<RefreshToken?> GetRefreshToken(string refreshToken, CancellationToken ct = default) => await _dbContext
        .RefreshTokens
        .AsNoTracking()
        .Include(token => token.User)
        .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken), ct);
    
    public async Task AddRefreshToken(RefreshToken refreshToken)
    { 
        var tokens = _dbContext.RefreshTokens
            .Where(token => token.UserId == refreshToken.UserId);
        
        _dbContext.RefreshTokens.RemoveRange(tokens);
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<Tenant?> GetTenantByInviteCode(InviteCode inviteCode, CancellationToken ct = default) => await _dbContext
        .Tenants
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == inviteCode.TenantId, ct);

    public async Task<InviteCode?> GetInviteCode(string code, CancellationToken ct = default) => await _dbContext
        .InviteCodes
        .AsNoTracking()
        .FirstOrDefaultAsync(inviteCode => inviteCode.Value.Equals(code), ct);

    public async Task AddInviteCode(InviteCode inviteCode, CancellationToken ct = default)
    {
        var inviteCodes = _dbContext.InviteCodes
            .Where(code => code.TenantId == inviteCode.TenantId);
        
        _dbContext.InviteCodes.RemoveRange(inviteCodes);
        await _dbContext.InviteCodes.AddAsync(inviteCode, ct);
    }
}