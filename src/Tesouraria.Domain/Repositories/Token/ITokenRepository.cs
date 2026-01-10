using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Repositories.Token;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshToken(string refreshToken);
    Task AddRefreshToken(RefreshToken refreshToken); 
}