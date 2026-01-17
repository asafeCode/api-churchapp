using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Application.UseCases.Commands.User.ChangePassword;

public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserUpdateRepository  _repository;
    private readonly ILoggedUser _loggedUser;
    
    public ChangePasswordHandler(
        IPasswordEncripter passwordEncripter, 
        IUserUpdateRepository repository, 
        ILoggedUser loggedUser)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<Unit> HandleAsync(ChangePasswordCommand command, CancellationToken ct = default)
    {
        var request = command.Request;
        var (loggedUserId, tenantId) = _loggedUser.User();
        var user = await _repository.GetUserById(loggedUserId, tenantId, ct);
        
        user.UpdatePassword( _passwordEncripter.Encrypt(request.NewPassword));
        
        return Unit.Value;
    }

}