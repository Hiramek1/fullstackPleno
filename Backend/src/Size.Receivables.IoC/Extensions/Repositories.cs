using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.Data.Repositories;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.IoC.Extensions;

public static class Repositories
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    => services
        .AddScoped<ICompanyRepository, CompanyRepository>()
        .AddScoped<IInvoiceRepository, InvoiceRepository>()
        .AddScoped<ICartRepository, CartRepository>();
}
