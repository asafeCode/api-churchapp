using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Token.RefreshToken;

public class CreateNewTokenHandler : ICommandHandler<CreateNewTokenCommand, ResponseTokensJson>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    
    public CreateNewTokenHandler(
        ITokenRepository tokenRepository, 
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _tokenRepository = tokenRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<ResponseTokensJson> HandleAsync(CreateNewTokenCommand command, CancellationToken ct = default)
    {
        var refreshToken = await _tokenRepository.GetRefreshToken(command.RefreshToken, ct);
        if (refreshToken is null)
            throw new RefreshTokenNotFoundException();
        
        if (DateTime.Compare(refreshToken.ExpiresOn, refreshToken.CreatedOn) < 0)
            throw new RefreshTokenExpiredException();

        var newRefreshToken = _refreshTokenGenerator.CreateToken(refreshToken.UserId, refreshToken.TenantId);
        
        await _tokenRepository.AddRefreshToken(newRefreshToken);

        return new ResponseTokensJson
        {
            AccessToken = _accessTokenGenerator.Generate(new JwtClaims(refreshToken.UserId, refreshToken.User.TenantId, refreshToken.User.Role.ToString())),
            RefreshToken = newRefreshToken.Value
        };
    }
}