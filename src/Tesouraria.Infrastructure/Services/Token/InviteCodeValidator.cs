using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Infrastructure.Services.Token;

public class InviteCodeValidator : IInviteCodeValidator
{
    private readonly ITokenRepository _tokenRepository;

    public InviteCodeValidator(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }
    public async Task<InviteCode> ValidateAndGetCode(string code, CancellationToken ct = default)
    {
        var inviteCode = await _tokenRepository.GetInviteCode(code, ct);
        if (inviteCode == null) throw new NotFoundException("Codigo de convite não encontrado");

        if (inviteCode.ExpiresOn <= DateTime.UtcNow)
            throw new ExpiredTokenException("O código expirou, fale com um administrador para gerar outro código de convite");

        return inviteCode;
    }
}