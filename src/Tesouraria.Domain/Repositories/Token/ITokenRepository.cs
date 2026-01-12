using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Repositories.Token;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshToken(string refreshToken, CancellationToken ct = default);
    Task AddRefreshToken(RefreshToken refreshToken); 
}