using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Queries.Reports;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Reports;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers;

[AuthenticatedUser(Roles.Admin)]
public class ReportController : TesourariaControllerBase
{
    [HttpGet]
    [Route("monthly-summary")]
    [ProducesResponseType(typeof(ResponseMonthlySummaryReadModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMonthlySummary([FromQuery] ReportFilterDto filter, [FromServices] IMediator mediator)
    {
        var query = new GetMontlySummaryQuery(filter);
        var response = await mediator.QueryAsync<GetMontlySummaryQuery, ResponseMonthlySummaryReadModel>(query);
        return Ok(response);
    }
}