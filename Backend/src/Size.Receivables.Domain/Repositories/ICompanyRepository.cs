using Size.Receivables.Domain.Entities;

namespace Size.Receivables.Domain.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByCnpjAsync(int id);
    Task<Company?> GetByCnpjAsync(string cnpj);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
}
