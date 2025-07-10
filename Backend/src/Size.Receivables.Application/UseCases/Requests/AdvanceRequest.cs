namespace Size.Receivables.Application.UseCases.Requests;

public class AdvanceRequest
{
    public string CompanyCnpj { get; set; } = string.Empty;
    public List<string> InvoiceNumbers { get; set; } = [];
}
