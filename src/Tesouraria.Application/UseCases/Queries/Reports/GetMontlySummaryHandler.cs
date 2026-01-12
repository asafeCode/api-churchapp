using FluentValidation;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses.Reports;
using Tesouraria.Domain.Repositories.Report;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.Reports;

public record GetMontlySummaryQuery(ReportFilterDto Filter) : IQuery;

public class GetMontlySummaryHandler : IQueryHandler<GetMontlySummaryQuery, ResponseMonthlySummaryReadModel>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IReportRepository  _reportRepository;

    public GetMontlySummaryHandler(ILoggedUser loggedUser, IReportRepository reportRepository)
    {
        _loggedUser = loggedUser;
        _reportRepository = reportRepository;
    }
    public async Task<ResponseMonthlySummaryReadModel> HandleAsync(GetMontlySummaryQuery query, CancellationToken ct = default)
    {
        if (query.Filter.DateFrom is null || query.Filter.DateTo is null) throw new ValidationException("Informe um período");
        var ( userId, tenantId) = _loggedUser.User();
        var report = await _reportRepository.GetMonthlySummary(tenantId, query.Filter, userId, ct);
        
        return report;
    }
}