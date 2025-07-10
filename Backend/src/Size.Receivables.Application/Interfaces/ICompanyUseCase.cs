using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;
using Size.Receivables.CrossCutting.Services;

namespace Size.Receivables.Application.Interfaces;

public interface ICompanyUseCase
{
    Task<CustomResult<RetrieveCompanyResponse>> GetAsync(string cnpj);
    Task<CustomResult<bool>> RegisterAsync(RegisterCompanyRequest request);
}
