using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Domain.Strategies.CalcularLimite;

public class ServicesMediumRevenueStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(decimal monthlyRevenue, BusinessType businessType)
        => monthlyRevenue >= 50_001m && monthlyRevenue <= 100_000m && businessType == BusinessType.Services;

    public decimal Calculate(decimal monthlyRevenue) => monthlyRevenue * 0.55m;
}
