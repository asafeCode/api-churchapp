using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Auth.UserLogin;

public class DoLoginHandler : ICommandHandler<DoLoginCommand, ResponseLoggedUserJson>
{
    private readonly IUserReadRepository  _readRepository;
    private readonly IPasswordEncripter  _passwordEncripter;
    private readonly IAccessTokenGenerator _tokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public DoLoginHandler(IUserReadRepository readRepository, 
        IPasswordEncripter passwordEncripter, 
        IAccessTokenGenerator tokenGenerator, 
        IRefreshTokenGenerator refreshTokenGenerator, 
        ITokenRepository tokenRepository)
    {
        _readRepository = readRepository;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }
    
    public async Task<ResponseLoggedUserJson> HandleAsync(DoLoginCommand request, CancellationToken ct = default)
    {
        var user = await _readRepository.GetUserByName(request.Name.NormalizeUsername(), request.TenantId, ct);
        if (user is null || _passwordEncripter.IsValid(request.Password, user.PasswordHash).IsFalse()) 
            throw new InvalidLoginException();
        
        var refreshToken = await CreateAndSaveRefreshToken(user.Id, user.TenantId);
        return ResponseLoggedUserJson(user, refreshToken, _tokenGenerator);
    }
    private async Task<RefreshToken> CreateAndSaveRefreshToken(Guid userId, Guid tenantId)
    {
        var refreshToken = _refreshTokenGenerator.CreateToken(userId, tenantId);
        await _tokenRepository.AddRefreshToken(refreshToken);
        return refreshToken;
    }
    private static ResponseLoggedUserJson ResponseLoggedUserJson(Domain.Entities.User user, RefreshToken refresh, IAccessTokenGenerator tokenGenerator)
    {
        return new ResponseLoggedUserJson
        {
            Name = user.Username,
            Role = user.Role,
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokenGenerator.Generate(new JwtClaims(user.Id, user.TenantId, user.Role.ToString())),
                RefreshToken = refresh.Value
            }
        };
    }
}