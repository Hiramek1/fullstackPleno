using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.Application.Interfaces;
using Size.Receivables.Application.UseCases;

namespace Size.Receivables.IoC.Extensions;

public static class UseCases
{
    public static IServiceCollection RegisterUseCases(this IServiceCollection services)
    => services
        .AddScoped<IAdvanceUseCase, AdvanceUseCase>()
        .AddScoped<ICompanyUseCase, CompanyUseCase>()
        .AddScoped<ICreditLimitUseCase, CreditLimitUseCase>()
        .AddScoped<IInvoiceUseCase, InvoiceUseCase>();
}
