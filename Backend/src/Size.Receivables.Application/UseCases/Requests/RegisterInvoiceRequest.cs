namespace Size.Receivables.Application.UseCases.Requests;

public class RegisterInvoiceRequest
{
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}
