using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;

namespace Size.Receivables.Api.Controllers;

public sealed partial class RegistrationController
{
    [HttpGet("Company/{cnpj}")]
    [ProducesResponseType(typeof(RetrieveCompanyResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterEmpresa(string cnpj)
    {
        var result = await companyUseCase.GetAsync(cnpj);
        return result.IsFailure
            ? BadRequest(result.Error)
            : Ok(result.Value);
    }

    [HttpPost("Company")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CadastrarEmpresa([FromBody] RegisterCompanyRequest cadastrarEmpresaRequest)
    {
        var result = await companyUseCase.RegisterAsync(cadastrarEmpresaRequest);
        return result.IsFailure
            ? BadRequest(result.Error)
            : Ok(result.Value);
    }
}
