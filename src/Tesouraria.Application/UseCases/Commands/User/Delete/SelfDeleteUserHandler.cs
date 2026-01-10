using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.User.Delete;

public class SelfDeleteUserHandler : ICommandHandler<SelfDeleteUserCommand>
{
    private readonly ILoggedUser  _loggedUser;
    private readonly IUserUpdateRepository _updateRepository;

    public SelfDeleteUserHandler(
        ILoggedUser loggedUser, 
        IUserUpdateRepository updateRepository
        )
    {
        _loggedUser = loggedUser;
        _updateRepository = updateRepository;
    }
    public async Task<Unit> HandleAsync(SelfDeleteUserCommand command, CancellationToken ct = default)
    {
        if (command.Force.IsFalse())
            throw new ConflictException(ResourceMessagesException.CONFIRMATION_REQUIRED_TO_DELETE_ACCOUNT);
        
        var (loggedUserId, tenantId) =  _loggedUser.User();
        var user = await _updateRepository.GetUserById(loggedUserId, tenantId, ct);
        
        user.Active = false;
        _updateRepository.Update(user);
        return Unit.Value;
    }
}