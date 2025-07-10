using Size.Receivables.Domain.Entities;

namespace Size.Receivables.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id);
    Task<IEnumerable<Invoice>> GetByIdsAsync(IEnumerable<int> ids);
    Task<Invoice?> GetByNumberAsync(string number);
    Task AddAsync(Invoice invoice);
}
