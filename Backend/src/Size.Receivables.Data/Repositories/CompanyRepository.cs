using Microsoft.EntityFrameworkCore;
using Size.Receivables.Data.Database;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.Data.Repositories;

public class CompanyRepository(ReceivablesDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetByCnpjAsync(int id)
        => await context.Companies.FindAsync(id);

    public async Task<Company?> GetByCnpjAsync(string cnpj)
        => await context.Companies
            .FirstOrDefaultAsync(c => c.Cnpj == cnpj);

    public async Task AddAsync(Company company)
    {
        await context.Companies.AddAsync(company);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        context.Companies.Update(company);
        await context.SaveChangesAsync();
    }
}
