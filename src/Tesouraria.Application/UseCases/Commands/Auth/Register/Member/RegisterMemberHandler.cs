using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Repositories.Tenant;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Auth.Register.Member;

public class RegisterMemberHandler : ICommandHandler<RegisterMemberCommand, ResponseLoggedUserJson>
{
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IInviteCodeValidator _codeValidator;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public RegisterMemberHandler(
        IPasswordEncripter passwordEncripter, 
        IUserWriteRepository writeRepository, 
        IRefreshTokenGenerator refreshTokenGenerator, 
        ITokenRepository tokenRepository, 
        IAccessTokenGenerator tokenGenerator, 
        IInviteCodeValidator codeValidator, 
        ITenantRepository tenantRepository)
    {
        _passwordEncripter = passwordEncripter;
        _writeRepository = writeRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
        _tokenGenerator = tokenGenerator;
        _codeValidator = codeValidator;
        _tenantRepository = tenantRepository;
    }
    public async Task<ResponseLoggedUserJson> HandleAsync(RegisterMemberCommand command, CancellationToken ct = default)
    {
        var inviteCode = await _codeValidator.ValidateAndGetCode(command.InviteCode, ct);
        
        var tenant = await _tenantRepository.GetTenantById(inviteCode.TenantId, ct);
        if (tenant == null) throw new NotFoundException("Tenant não encontrado");
        
        var user = command.ToMember(_passwordEncripter, tenant.Id);
        await _writeRepository.AddUserAsync(user);
        var refreshToken = _refreshTokenGenerator.CreateToken(user.Id, user.TenantId);
        await _tokenRepository.AddRefreshToken(refreshToken);
        
        return user.ToLoggedResponse(refreshToken, _tokenGenerator);
    }
}