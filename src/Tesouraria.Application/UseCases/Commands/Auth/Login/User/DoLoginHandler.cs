using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Auth.Login.User;

public class DoLoginHandler : ICommandHandler<DoLoginCommand, ResponseLoggedUserJson>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IPasswordEncripter _passwordEncripter;
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

        var refreshToken = _refreshTokenGenerator.CreateToken(user.Id, user.TenantId);

        await _tokenRepository.AddRefreshTokenSafe(refreshToken, ct);
        
        return user.ToLoggedResponse(refreshToken, _tokenGenerator);
    }
}