using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Application.Interfaces;

public interface ICreditLimitUseCase
{
    decimal DetermineLimit(decimal monthlyRevenue, BusinessType businessType);
}
