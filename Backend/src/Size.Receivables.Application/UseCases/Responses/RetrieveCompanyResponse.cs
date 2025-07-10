using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Application.UseCases.Responses;

public class RetrieveCompanyResponse
{
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public BusinessType BusinessCategory { get; set; }
    public decimal CreditLimit { get; set; }
}
