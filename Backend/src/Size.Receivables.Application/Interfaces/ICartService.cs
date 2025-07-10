namespace Size.Receivables.Application.Interfaces;

public interface ICartService
{
    Task AddInvoiceAsync(string companyCnpj, string invoiceNumber);
    Task RemoveInvoiceAsync(string companyCnpj, string invoiceNumber);
    Task<List<string>> GetCartAsync(string companyCnpj);
    Task ClearCartAsync(string companyCnpj);
}
