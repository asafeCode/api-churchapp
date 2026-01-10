using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Expense.Create;
using Tesouraria.Application.UseCases.Queries.ExpenseDashboard;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Expenses;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin)]
public class ExpenseController : TesourariaControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpenseJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateExpense([FromServices] IMediator handler, [FromBody] CreateExpenseCommand command)
    {
        var result = await handler.SendAsync<CreateExpenseCommand, ResponseRegisteredExpenseJson>(command);
        return Created(string.Empty, result);
    }  
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll([FromServices] IMediator handler)
    {
        var query = new GetExpenseDashboardQuery();
        var result = await handler.QueryAsync<GetExpenseDashboardQuery, ResponseExpensesJson>(query);
        return Ok(result);
    }   
}