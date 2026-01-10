using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Worships;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Worship;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Worship.Create;

public class CreateWorshipHandler : ICommandHandler<CreateWorshipCommand, ResponseRegisteredWorshipJson>
{
    private readonly IWorshipRepository _worshipRepository;
    private readonly ILoggedUser _loggedUser;

    public CreateWorshipHandler(IWorshipRepository worshipRepository, ILoggedUser loggedUser)
    {
        _worshipRepository = worshipRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredWorshipJson> HandleAsync(CreateWorshipCommand command, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var worship = command.ToWorship(tenantId);

        await _worshipRepository.Add(worship, ct);
        return new ResponseRegisteredWorshipJson(worship.Id);
    }
}