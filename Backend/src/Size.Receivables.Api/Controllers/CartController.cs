using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.Interfaces;

namespace Size.Receivables.Api.Controllers;

/// <summary>
/// Utilizado para controle de itens no carrinho
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed partial class CartController (ICartService cartService) : ControllerBase;