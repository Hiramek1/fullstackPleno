using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Domain.Strategies.CalcularLimite;

public class ProductsLargeRevenueStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(decimal monthlyRevenue, BusinessType businessType)
        => monthlyRevenue > 100_000m && businessType == BusinessType.Products;

    public decimal Calculate(decimal monthlyRevenue) => monthlyRevenue * 0.65m;
}
