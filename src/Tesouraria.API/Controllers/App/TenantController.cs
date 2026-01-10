using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Tenants.Create;
using Tesouraria.Application.UseCases.Queries.TenantsDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Tenants;

namespace Tesouraria.API.Controllers.App;

public class TenantController : TesourariaControllerBase
{
    [HttpPost]
    [AuthenticatedOwner]
    [ProducesResponseType(typeof(ResponseCreatedTenantJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTenant([FromServices] IMediator mediator,
        [FromBody] CreateTenantCommand command)
    {
        var result = await mediator.SendAsync<CreateTenantCommand, ResponseCreatedTenantJson>(command);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [Route("/tenants")]
    [ProducesResponseType(typeof(ResponseTenantsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTenants([FromServices] IMediator mediator)
    {
        var query = new GetTenantsDashboardQuery();
        var result = await mediator.QueryAsync<GetTenantsDashboardQuery, ResponseTenantsJson>(query);
        return Ok(result);
    }    
    
    // [HttpPut]
    // [AuthenticatedOwner]
    // [Route("tenants")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    // public async Task<IActionResult> UpdateTenant([FromServices] IMediator mediator, [FromBody] UpdateTenantCommand command)
    // {
    //     await mediator.SendAsync<UpdateTenantCommand>(command);
    //     return NoContent();
    // }
}