using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Tesouraria.Infrastructure.Services.Token;

public abstract class JwtTokenHandler
{
    protected JwtTokenHandler() {}
    protected static SymmetricSecurityKey SecurityKey(string signingKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signingKey);
        return new SymmetricSecurityKey(bytes);
    }
}