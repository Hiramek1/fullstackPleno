using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;

namespace Size.Receivables.Api.Controllers;

public sealed partial class AdvanceController
{
    [HttpPost("Calculate")]
    [ProducesResponseType(typeof(AdvanceResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Calcular([FromBody] AdvanceRequest antecipacaoResultadoRequest)
    {
        var result = await advanceUseCase.ExecuteAsync(antecipacaoResultadoRequest);
        return result.IsFailure
            ? BadRequest(result.Error)
            : Ok(result.Value);
    }
}
