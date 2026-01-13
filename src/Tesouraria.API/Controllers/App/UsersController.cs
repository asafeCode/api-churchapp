using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Users.Delete;
using Tesouraria.Application.UseCases.Queries.GetUserById;
using Tesouraria.Application.UseCases.Queries.UsersDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin)]
public class UsersController : TesourariaControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUsersJson),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromServices] IMediator handler, [FromQuery] UserFilterDto filters)
    {
        var query = new UserDashboardQuery(filters);
        var result = await handler.QueryAsync<UserDashboardQuery, ResponseUsersJson>(query);
        return Ok(result);
    }     
    
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ResponseUserJson),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserWithId([FromServices] IMediator handler, [FromRoute]  Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await handler.QueryAsync<GetUserByIdQuery,ResponseUserJson>(query);
        return Ok(result);
    }     
    
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteUser([FromServices] IMediator handler, 
        [FromQuery] bool force, [FromRoute] Guid id)
    {
        var command = new DeleteUserCommand(force, id); 
        await handler.SendAsync(command);
        return NoContent();
    }    
}