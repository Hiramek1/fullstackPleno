using Size.Receivables.Application.Interfaces;
using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.CrossCutting.Services;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.Application.UseCases;

public class InvoiceUseCase(ICompanyRepository companyRepository,
                            IInvoiceRepository invoiceRepository) : IInvoiceUseCase
{
    public async Task<CustomResult<bool>> Execute(string cnpj, RegisterInvoiceRequest request)
    {
        var company = await companyRepository.GetByCnpjAsync(cnpj);
        if (company is null)
            return CustomResult<bool>.Fail("Empresa não encontrada");

        try
        {
            var invoice = new Invoice(
                company.Id,
                request.Number,
                request.Amount,
                request.DueDate
            );

            company.AddInvoice(invoice);
            await invoiceRepository.AddAsync(invoice);

            return CustomResult<bool>.Ok(true);
        }
        catch (ArgumentException ex)
        {
            return CustomResult<bool>.Fail(ex.Message);
        }
    }
}
