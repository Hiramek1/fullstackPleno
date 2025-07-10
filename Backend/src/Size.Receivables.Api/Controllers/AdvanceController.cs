using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.Interfaces;

namespace Size.Receivables.Api.Controllers;

/// <summary>
/// Utilizado para cálcular adiantamento de recebíveis
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed partial class AdvanceController(IAdvanceUseCase advanceUseCase) : ControllerBase;