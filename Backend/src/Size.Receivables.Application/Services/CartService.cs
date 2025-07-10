using Size.Receivables.Application.Interfaces;
using Size.Receivables.Domain.Repositories;
using Size.Receivables.Domain.ValueObjects;

namespace Size.Receivables.Application.Services;

public class CartService(ICartRepository cartRepository,
                         ICompanyRepository companyRepository,
                         IInvoiceRepository invoiceRepository) : ICartService
{
    public async Task AddInvoiceAsync(string companyCnpj, string invoiceNumber)
    {
        var company = await companyRepository.GetByCnpjAsync(companyCnpj)
            ?? throw new Exception("Companhia não encontrada.");

        var invoice = await invoiceRepository.GetByNumberAsync(invoiceNumber)
            ?? throw new Exception("Nota Fiscal inválida.");

        var cartItem = new CartItem
        {
            CompanyId = company.Id,
            InvoiceId = invoice.Id
        };

        await cartRepository.AddItemAsync(cartItem);
    }

    public async Task RemoveInvoiceAsync(string companyCnpj, string invoiceNumber)
    {
        var company = await companyRepository.GetByCnpjAsync(companyCnpj)
            ?? throw new Exception("Companhia não encontrada.");

        var invoice = await invoiceRepository.GetByNumberAsync(invoiceNumber)
            ?? throw new Exception("Nota Fiscal inválida.");

        await cartRepository.RemoveItemAsync(company.Id, invoice.Id);
    }

    public async Task<List<string>> GetCartAsync(string companyCnpj)
    {
        var company = await companyRepository.GetByCnpjAsync(companyCnpj)
            ?? throw new Exception("Companhia não encontrada.");

        var cartItems = await cartRepository.GetItemsAsync(company.Id);
        var invoiceIds = cartItems.Select(ci => ci.InvoiceId).ToList();

        var invoices = await invoiceRepository.GetByIdsAsync(invoiceIds);
        return invoices.Select(i => i.Number).ToList();
    }

    public async Task ClearCartAsync(string companyCnpj)
    {
        var company = await companyRepository.GetByCnpjAsync(companyCnpj)
            ?? throw new Exception("Companhia não encontrada.");

        await cartRepository.ClearAsync(company.Id);
    }
}
