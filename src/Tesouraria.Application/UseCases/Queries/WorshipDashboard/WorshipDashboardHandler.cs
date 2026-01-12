using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Worships;
using Tesouraria.Domain.Repositories.Worship;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.WorshipDashboard;

public record GetWorshipDashboardQuery : IQuery;
public class WorshipDashboardHandler : IQueryHandler<GetWorshipDashboardQuery, ResponseWorshipsJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IWorshipRepository _repository;

    public WorshipDashboardHandler(IWorshipRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseWorshipsJson> HandleAsync(GetWorshipDashboardQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var worships = await _repository.GetAll(tenantId, ct);

        var response = worships.Select(worship => new ResponseWorshipJson
        { 
            Id = worship.Id,
            DayOfWeek = worship.DayOfWeek,
            Description = worship.Description,
            Time = worship.Time,
        });

        return new ResponseWorshipsJson
        {
            Worships = response
        };
    }
}