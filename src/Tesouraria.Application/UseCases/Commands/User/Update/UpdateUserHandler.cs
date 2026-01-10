using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.User.Update;

public class UpdateUserHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserUpdateRepository _updateRepository;
    private readonly ILoggedUser  _loggedUser;

    public UpdateUserHandler(
        IUserUpdateRepository updateRepository, 
        ILoggedUser loggedUser)
    {
        _updateRepository = updateRepository;
        _loggedUser = loggedUser;
    }
    public async Task<Unit> HandleAsync(UpdateUserCommand command, CancellationToken ct = default)
    {
        var (loggedUserId, tenantId) = _loggedUser.User();
        var user = await _updateRepository.GetUserById(loggedUserId, tenantId, ct);
        
        command.ToUpdatedUser(user);
        _updateRepository.Update(user);
        return Unit.Value;
    }

}