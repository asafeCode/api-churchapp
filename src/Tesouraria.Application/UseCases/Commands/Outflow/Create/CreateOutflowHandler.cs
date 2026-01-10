using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Repositories.Outflow;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Commands.Outflow.Create;

public class CreateOutflowHandler : ICommandHandler<CreateOutflowCommand, ResponseCreatedOutflowJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IOutflowRepository _outflowRepository;

    public CreateOutflowHandler(
        ILoggedUser loggedUser, 
        IOutflowRepository outflowRepository
        )
    {
        _loggedUser = loggedUser;
        _outflowRepository = outflowRepository;
    }
    public async Task<ResponseCreatedOutflowJson> HandleAsync(CreateOutflowCommand command, CancellationToken ct = default)
    {
        var (loggedUserId, tenantId) =  _loggedUser.User();
        var outflow = command.ToOutflow(loggedUserId, tenantId);
        
        await _outflowRepository.Add(outflow, ct);

        return new ResponseCreatedOutflowJson(outflow.Id, outflow.Amount);
    }
}