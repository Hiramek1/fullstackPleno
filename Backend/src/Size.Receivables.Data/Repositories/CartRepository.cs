using Microsoft.EntityFrameworkCore;
using Size.Receivables.Domain.ValueObjects;
using Size.Receivables.Domain.Repositories;
using Size.Receivables.Data.Database;

namespace Size.Receivables.Data.Repositories;

public class CartRepository(ReceivablesDbContext context) : ICartRepository
{
    public async Task AddItemAsync(CartItem cartItem)
    {
        context.CartItems.Add(cartItem);
        await context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int companyId, int invoiceId)
    {
        var item = await context.CartItems
            .FirstOrDefaultAsync(ci => ci.CompanyId == companyId && ci.InvoiceId == invoiceId);

        if (item != null)
        {
            context.CartItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CartItem>> GetItemsAsync(int companyId)
    {
        return await context.CartItems
            .Where(ci => ci.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task ClearAsync(int companyId)
    {
        var items = await context.CartItems
            .Where(ci => ci.CompanyId == companyId)
            .ToListAsync();

        context.CartItems.RemoveRange(items);
        await context.SaveChangesAsync();
    }
}
