using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Infrastructure.Services.Logged;
public class LoggedUser : ILoggedUser
{
    private readonly ITokenProvider _token;
    private readonly IAccessTokenValidator _tokenValidator;

    public LoggedUser( 
        ITokenProvider token, 
        IAccessTokenValidator tokenValidator)
    {
        _token = token;
        _tokenValidator = tokenValidator;
    }

    public (Guid userId, Guid tenantId) User()
    {
        var token = _token.Value();
        var claims = _tokenValidator.ValidateUserToken(token);
        return (claims.UserId, claims.TenantId);
    }
}