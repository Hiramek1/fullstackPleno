using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Domain.Strategies.CalcularLimite;

public interface ICreditLimitStrategy
{
    bool AppliesTo(decimal monthlyRevenue, BusinessType businessType);
    decimal Calculate(decimal monthlyRevenue);
}
