using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.CrossCutting.Services;

namespace Size.Receivables.Application.Interfaces;

public interface IInvoiceUseCase
{
    Task<CustomResult<bool>> Execute(string cnpj, RegisterInvoiceRequest request);
}
