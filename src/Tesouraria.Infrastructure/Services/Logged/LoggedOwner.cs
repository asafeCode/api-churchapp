using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Repositories.Owner;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Infrastructure.Services.Logged;

public class LoggedOwner : ILoggedOwner
{
    private readonly ITokenProvider _token;
    private readonly IAccessTokenValidator _tokenValidator;

    public LoggedOwner(
        ITokenProvider token, 
        IAccessTokenValidator tokenValidator)
    {
        _token = token;
        _tokenValidator = tokenValidator;
    }
    public Guid Owner()
    {
        var token = _token.Value();
        var claims = _tokenValidator.ValidateOwnerToken(token);
        return claims.OwnerId;
    }
}