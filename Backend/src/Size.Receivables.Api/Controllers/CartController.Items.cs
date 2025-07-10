using Microsoft.AspNetCore.Mvc;
using Size.Receivables.Application.UseCases.Requests;

namespace Size.Receivables.Api.Controllers;

public sealed partial class CartController
{
    [HttpPost("{companyCnpj}/items")]
    public async Task<IActionResult> AddToCart(string companyCnpj, [FromBody] AddToCartRequest request)
    {
        await cartService.AddInvoiceAsync(companyCnpj, request.InvoiceNumber);
        return Ok();
    }

    [HttpDelete("{companyCnpj}/items/{invoiceNumber}")]
    public async Task<IActionResult> RemoveFromCart(string companyCnpj, string invoiceNumber)
    {
        await cartService.RemoveInvoiceAsync(companyCnpj, invoiceNumber);
        return Ok();
    }

    [HttpGet("{companyCnpj}")]
    public async Task<IActionResult> GetCart(string companyCnpj)
    {
        var cart = await cartService.GetCartAsync(companyCnpj);
        return Ok(cart);
    }
}
