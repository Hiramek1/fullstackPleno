using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;
using Size.Receivables.CrossCutting.Services;

namespace Size.Receivables.Application.Interfaces;

public interface IAdvanceUseCase
{
    Task<CustomResult<AdvanceResponse>> ExecuteAsync(AdvanceRequest request);
}
