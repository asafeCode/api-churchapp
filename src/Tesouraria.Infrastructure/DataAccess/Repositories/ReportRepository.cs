using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Repositories.Report;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly TesourariaDbContext _context;
    public ReportRepository(TesourariaDbContext context) => _context = context;

    public async Task<ResponseMonthlySummaryReadModel> GetMonthlySummary(
        Guid tenantId,
        ReportFilterDto filter,
        Guid userId,
        CancellationToken ct)
    {
        var createdByUser = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId && u.TenantId == tenantId)
            .Select(u => u.Username)
            .FirstOrDefaultAsync(ct);

        var inflowsByType = await _context.Inflows
            .AsNoTracking()
            .Where(i => i.TenantId == tenantId &&
                        i.Date >= filter.DateFrom &&
                        i.Date <= filter.DateTo)
            .GroupBy(i => i.Type)
            .Select(g => new
            {
                Type = g.Key,
                Total = g.Sum(i => i.Amount)
            })
            .ToListAsync(ct);

        var outflowsByExpenseType = await _context.Outflows
            .AsNoTracking()
            .Where(o => o.TenantId == tenantId &&
                        o.Date >= filter.DateFrom &&
                        o.Date <= filter.DateTo)
            .GroupBy(o => o.Expense.Type)
            .Select(g => new
            {
                Type = g.Key,
                Total = g.Sum(o => o.Amount)
            })
            .ToListAsync(ct);

        var installmentExpenses = await _context.Outflows
            .AsNoTracking()
            .Where(o => o.TenantId == tenantId &&
                        o.Date >= filter.DateFrom &&
                        o.Date <= filter.DateTo &&
                        o.Expense.Type == ExpenseType.Installment)
            .Select(o => new
            {
                o.Expense.Id,
                o.Expense.Name,
                o.Expense.CurrentInstallment,
                o.Expense.TotalInstallments,
                InstallmentAmount = o.Expense.AmountOfEachInstallment ?? o.Amount
            })
            .GroupBy(x => new
            {
                x.Id,
                x.Name,
                x.CurrentInstallment,
                x.TotalInstallments,
                x.InstallmentAmount
            })
            .Select(g => new InstallmentExpenseDetail
            {
                Name = g.Key.Name,
                CurrentInstallment = g.Key.CurrentInstallment ?? 0,
                TotalInstallments = g.Key.TotalInstallments ?? 0,
                InstallmentAmount = g.Key.InstallmentAmount
            })
            .ToListAsync(ct);
        

        var inflowsAmount = new InflowsAmountPerType
        {
            Tithe = inflowsByType
                .FirstOrDefault(x => x.Type == InflowType.Tithe)?.Total ?? 0,

            Offering = inflowsByType
                .FirstOrDefault(x => x.Type == InflowType.Offering)?.Total ?? 0,

            Other = inflowsByType
                .FirstOrDefault(x => x.Type == InflowType.Other)?.Total ?? 0
        };

        var outflowsAmount = new OutflowsAmountPerExpenseType
        {
            Fixed = outflowsByExpenseType
                .FirstOrDefault(x => x.Type == ExpenseType.Fixed)?.Total ?? 0,

            Variable = outflowsByExpenseType
                .FirstOrDefault(x => x.Type == ExpenseType.Variable)?.Total ?? 0,

            Installment = outflowsByExpenseType
                .FirstOrDefault(x => x.Type == ExpenseType.Installment)?.Total ?? 0
        };

        var totalInflowsAmount = inflowsByType.Sum(x => x.Total);
        var totalOutflowsAmount = outflowsByExpenseType.Sum(x => x.Total);
        var balance = totalInflowsAmount - totalOutflowsAmount;

        var period = $"{filter.DateFrom:MM/yyyy} a {filter.DateTo:MM/yyyy}";

        return new ResponseMonthlySummaryReadModel
        {
            Period = period,
            TotalInflowsAmount = totalInflowsAmount,
            TotalOutflowsAmount = totalOutflowsAmount,
            Balance = balance,
            InflowsAmountPerType = inflowsAmount,
            OutflowsAmountPerExpenseType = outflowsAmount,
            InstallmentExpensesDetails = new InstallmentExpensesDetails
            {
                InstallmentExpenses = installmentExpenses
            },
            CreatedBy = createdByUser!
        };
    }
}