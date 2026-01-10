using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Outflow.Create;
using Tesouraria.Application.UseCases.Queries.OutflowDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin)]
public class OutflowController : TesourariaControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedOutflowJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateInflow([FromServices] IMediator handler, [FromBody] CreateOutflowCommand command)
    {
        var result = await handler.SendAsync<CreateOutflowCommand, ResponseCreatedOutflowJson>(command);
        return Created(string.Empty, result);
    }    
    
    [HttpGet]
    [Route("/Outflows")]
    [ProducesResponseType(typeof(ResponseOutflowsJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Dashboard([FromServices] IMediator handler, [FromQuery] OutflowFilterDto filter, CancellationToken ct)
    {
        var query = new OutflowDashboardQuery(filter);
        var result = await handler.QueryAsync<OutflowDashboardQuery, ResponseOutflowsJson>(query, ct);
        return Ok(result);
    }
}