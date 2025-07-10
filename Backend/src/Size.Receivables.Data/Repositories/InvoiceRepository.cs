using Microsoft.EntityFrameworkCore;
using Size.Receivables.Data.Database;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.Data.Repositories;

public class InvoiceRepository(ReceivablesDbContext context) : IInvoiceRepository
{
    public async Task AddAsync(Invoice invoice)
    {
        var company = await context.Companies
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.Id == invoice.CompanyId);

        company?.AddInvoice(invoice);
        await context.SaveChangesAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
        => await context.Invoices.FindAsync(id);

    public async Task<Invoice?> GetByNumberAsync(string number)
        => await context.Invoices
            .FirstOrDefaultAsync(i => i.Number == number);

    public async Task<IEnumerable<Invoice>> GetByIdsAsync(IEnumerable<int> ids)
        => await context.Invoices
            .Where(n => ids.Contains(n.Id))
            .ToListAsync();
}
