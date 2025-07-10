using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Domain.Strategies.CalcularLimite;

public class SmallRevenueStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(decimal monthlyRevenue, BusinessType businessType)
        => monthlyRevenue >= 10_000m && monthlyRevenue <= 50_000m;

    public decimal Calculate(decimal monthlyRevenue) => monthlyRevenue * 0.50m;
}
