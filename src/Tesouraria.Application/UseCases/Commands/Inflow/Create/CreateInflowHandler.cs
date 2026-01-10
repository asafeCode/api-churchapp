using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Repositories.Inflow;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Inflow.Create;

public class CreateInflowHandler : ICommandHandler<CreateInflowCommand, ResponseCreatedInflowJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IInflowRepository _inflowRepository;

    public CreateInflowHandler(
        ILoggedUser loggedUser, 
        IInflowRepository inflowRepository)
    {
        _loggedUser = loggedUser;
        _inflowRepository = inflowRepository;
    }
    public async Task<ResponseCreatedInflowJson> HandleAsync(CreateInflowCommand command, CancellationToken ct = default)
    {
        var (loggedUserId, tenantId) = _loggedUser.User();
        var inflow = command.ToInflow(loggedUserId, tenantId);
        
        await _inflowRepository.Add(inflow, ct);
        return new ResponseCreatedInflowJson(inflow.Id, inflow.Amount);
    }
}