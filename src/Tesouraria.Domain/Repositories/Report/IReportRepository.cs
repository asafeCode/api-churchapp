using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.Responses.Reports;

namespace Tesouraria.Domain.Repositories.Report;

public interface IReportRepository
{
    Task<ResponseMonthlySummaryReadModel> GetMonthlySummary(Guid tenantId, ReportFilterDto filter, Guid userId , CancellationToken ct);
}
