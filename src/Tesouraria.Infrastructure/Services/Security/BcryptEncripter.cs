using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Infrastructure.Services.Security;

public sealed class BcryptEncripter : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}