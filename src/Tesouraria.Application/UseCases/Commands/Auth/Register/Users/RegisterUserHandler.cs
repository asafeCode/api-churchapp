using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Users.Create;
public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, ResponseRegisteredUserJson>
{
    private readonly IUserWriteRepository  _writeRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    private readonly ILoggedUser _loggedUser;

    public RegisterUserHandler(
        IUserWriteRepository writeRepository, 
        IPasswordEncripter passwordEncripter, 
        IRefreshTokenGenerator refreshTokenGenerator, 
        ITokenRepository tokenRepository, ILoggedUser loggedUser)
    {
        _writeRepository = writeRepository;
        _passwordEncripter = passwordEncripter;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredUserJson> HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {
        var (_,tenantId) = _loggedUser.User();
        var user = command.ToUser(_passwordEncripter, tenantId);
        await _writeRepository.AddUserAsync(user);
        var refreshToken = _refreshTokenGenerator.CreateToken(user.Id, user.TenantId);
        await _tokenRepository.AddRefreshToken(refreshToken);
        return new ResponseRegisteredUserJson(user.Id, user.Username);
    }

}