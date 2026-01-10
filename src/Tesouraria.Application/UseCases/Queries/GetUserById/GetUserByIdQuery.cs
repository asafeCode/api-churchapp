using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery;