using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.Interfaces;

namespace Size.Receivables.Api.Controllers;

/// <summary>
/// Utilizado para cadastro de dados
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed partial class RegistrationController(ICompanyUseCase companyUseCase,
                                                   IInvoiceUseCase invoiceUseCase) : ControllerBase;
