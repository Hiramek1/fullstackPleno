using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Application.UseCases.Requests;

public class RegisterCompanyRequest
{
    public string Cnpj { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public BusinessType BusinessType { get; set; }
}
