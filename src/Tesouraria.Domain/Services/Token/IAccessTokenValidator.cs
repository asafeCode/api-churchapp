namespace Tesouraria.Domain.Services.Token;

public interface IAccessTokenValidator
{
    public JwtClaims ValidateUserToken(string token);
    public OwnerJwtClaims ValidateOwnerToken(string token);
}