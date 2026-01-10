using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Worship.Create;
using Tesouraria.Application.UseCases.Queries.WorshipDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Worships;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin)]
public class WorshipController : TesourariaControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredWorshipJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateWorship([FromServices] IMediator handler, [FromBody] CreateWorshipCommand command)
    {
        var result = await handler.SendAsync<CreateWorshipCommand, ResponseRegisteredWorshipJson>(command);
        return Created(string.Empty, result);
    }     
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseWorshipsJson),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll([FromServices] IMediator handler)
    {
        var query = new GetWorshipDashboardQuery();
        var result = await handler.QueryAsync<GetWorshipDashboardQuery, ResponseWorshipsJson>(query);
        return Ok(result);
    }   
}