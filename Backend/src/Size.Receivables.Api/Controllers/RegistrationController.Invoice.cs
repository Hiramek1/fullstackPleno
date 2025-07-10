using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.UseCases.Requests;

namespace Size.Receivables.Api.Controllers;

public sealed partial class RegistrationController
{
    [HttpPost("Company/{cnpj}/Invoice")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterInvoice(string cnpj, [FromBody] RegisterInvoiceRequest request)
    {
        var result = await invoiceUseCase.Execute(cnpj, request);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
