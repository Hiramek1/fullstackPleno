using Size.Receivables.Application.Interfaces;
using Size.Receivables.Domain.Enums;
using Size.Receivables.Domain.Strategies.CalcularLimite;

namespace Size.Receivables.Application.UseCases;

public class CreditLimitUseCase(IEnumerable<ICreditLimitStrategy> creditLimitStrategies) : ICreditLimitUseCase
{
    public decimal DetermineLimit(decimal monthlyRevenue, BusinessType businessType)
    {
        var estrategia = creditLimitStrategies.FirstOrDefault(e => e.AppliesTo(monthlyRevenue, businessType))
            ?? throw new InvalidOperationException("Limite não aplicável");

        return estrategia.Calculate(monthlyRevenue);
    }
}
