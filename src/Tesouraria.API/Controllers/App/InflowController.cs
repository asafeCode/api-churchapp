using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Inflow.Create;
using Tesouraria.Application.UseCases.Queries.InflowDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin)]
public class InflowController : TesourariaControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedInflowJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateInflow([FromServices] IMediator handler, [FromBody] CreateInflowCommand command)
    {
        var result = await handler.SendAsync<CreateInflowCommand, ResponseCreatedInflowJson>(command);
        return Created(string.Empty, result);
    }    
    
    [HttpGet]
    [Route("/Inflows")]
    [ProducesResponseType(typeof(ResponseInflowsJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInflowsJson),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Dashboard([FromServices] IMediator handler, [FromQuery] InflowFilterDto filter, CancellationToken ct)
    {
        var query = new InflowDashboardQuery(filter);
        var result = await handler.QueryAsync<InflowDashboardQuery, ResponseInflowsJson>(query, ct);
        
        return Ok(result);
    }

}