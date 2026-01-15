using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.Tenant;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Infrastructure.Services.Token;

public class InviteCodeValidator : IInviteCodeValidator
{
    private readonly ITokenRepository _tokenRepository;
    private readonly ITenantRepository _tenantRepository;

    public InviteCodeValidator(
        ITokenRepository tokenRepository, 
        ITenantRepository tenantRepository)
    {
        _tokenRepository = tokenRepository;
        _tenantRepository = tenantRepository;
    }
    public async Task<InviteCodeReadModel> ValidateAndGetCode(string code, CancellationToken ct = default)
    {
        var inviteCode = await _tokenRepository.GetInviteCode(code, ct);
        if (inviteCode == null) throw new NotFoundException("Codigo de convite não encontrado");

        if (inviteCode.ExpiresOn <= DateTime.UtcNow)
            throw new ExpiredTokenException("O código expirou, fale com um administrador para gerar outro código de convite");
        
        var exist = await _tenantRepository.ExistTenantWithId(inviteCode.TenantId, ct);
        
        return exist.IsFalse() ? throw new NotFoundException("Tenant inválido") : inviteCode;
    }
}