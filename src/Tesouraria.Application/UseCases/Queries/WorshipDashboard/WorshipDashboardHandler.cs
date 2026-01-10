using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Worships;
using Tesouraria.Domain.Repositories.Worship;

namespace Tesouraria.Application.UseCases.Queries.WorshipDashboard;

public record GetWorshipDashboardQuery : IQuery;
public class WorshipDashboardHandler : IQueryHandler<GetWorshipDashboardQuery, ResponseWorshipsJson>
{
    private readonly IWorshipRepository _repository;

    public WorshipDashboardHandler(IWorshipRepository repository) =>  _repository = repository; 
    public async Task<ResponseWorshipsJson> HandleAsync(GetWorshipDashboardQuery query, CancellationToken ct = default)
    {
        var worships = await _repository.GetAll(ct);

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