using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Users.Delete;

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteUserHandler(
        IUserReadRepository readRepository, 
        IUserWriteRepository writeRepository, ILoggedUser loggedUser)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _loggedUser = loggedUser;
    }
    public async Task<Unit> HandleAsync(DeleteUserCommand command, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        if (command.Force.IsFalse())
            throw new ConflictException(ResourceMessagesException.CONFIRMATION_REQUIRED_TO_DELETE_ACCOUNT);
        
        var exists = await _readRepository.ExistActiveUserWithId(command.UserId, tenantId, ct);

        if (exists.IsFalse())
            throw new NotFoundException("Usuário não encontrado");
        
        await _writeRepository.DeleteAccount(command.UserId, ct);
        return Unit.Value;
    }
}