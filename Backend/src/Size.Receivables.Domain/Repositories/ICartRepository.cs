using Size.Receivables.Domain.ValueObjects;

namespace Size.Receivables.Domain.Repositories;

public interface ICartRepository
{
    Task AddItemAsync(CartItem cartItem);
    Task RemoveItemAsync(int companyId, int invoiceId);
    Task<IEnumerable<CartItem>> GetItemsAsync(int companyId);
    Task ClearAsync(int companyId);
}
