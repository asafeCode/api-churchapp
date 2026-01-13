namespace Tesouraria.Domain.Dtos.ReadModels;

public record ResponseMonthlySummaryReadModel
{
    public string Period { get; init; } = string.Empty;
    
    public decimal TotalInflowsAmount { get; init; }
    public decimal TotalOutflowsAmount { get; init; }
    public decimal Balance { get; init; }
    
    public InflowsAmountPerType InflowsAmountPerType { get; init; } = default!;

    public OutflowsAmountPerExpenseType OutflowsAmountPerExpenseType { get; init; } = default!;
    
    public InstallmentExpensesDetails  InstallmentExpensesDetails { get; init; } = default!;
    
    public string CreatedBy { get; init; } = string.Empty;
};

public record InflowsAmountPerType
{
    public decimal Tithe { get; init; }
    public decimal Offering { get; init; }
    public decimal Other { get; init; }
}

public record OutflowsAmountPerExpenseType
{
    public decimal Fixed { get; init; }
    public decimal Variable { get; init; }
    public decimal Installment { get; init; }
}

public record InstallmentExpensesDetails
{
    public IEnumerable<InstallmentExpenseDetail> InstallmentExpenses { get; init; } = [];
}

public record InstallmentExpenseDetail
{
    public string Name { get; init; } =  string.Empty;
    public int CurrentInstallment { get; init; }
    public int TotalInstallments { get; init; }
    public decimal InstallmentAmount { get; init; }
}