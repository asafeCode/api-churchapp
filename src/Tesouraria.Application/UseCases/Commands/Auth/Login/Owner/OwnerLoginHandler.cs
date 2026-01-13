using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.Owner;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Auth.Login.Owner;

public class OwnerLoginHandler : ICommandHandler<OwnerLoginCommand, ResponseLoggedOwnerJson>
{
    private readonly IOwnerRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public OwnerLoginHandler(IOwnerRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<ResponseLoggedOwnerJson> HandleAsync(OwnerLoginCommand command, CancellationToken ct = default)
    {
        var owner = await _repository.GetOwnerWithEmail(command.Email, ct);
        if (owner is null || _passwordEncripter.IsValid(command.Password, owner.PasswordHash).IsFalse()) 
            throw new InvalidLoginException();
        
        return ResponseLoggedJson(owner);
    }
    private ResponseLoggedOwnerJson ResponseLoggedJson(Domain.Entities.Globals.Owner owner)
    {
        return new ResponseLoggedOwnerJson
        {
            Name = owner.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _tokenGenerator.GenerateOwner(new OwnerJwtClaims(owner.Id)),
                RefreshToken = "refresh.Value"
            }
        };
    }
}