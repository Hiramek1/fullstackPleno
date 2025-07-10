using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.Domain.Strategies.CalcularLimite;

namespace Size.Receivables.IoC.Extensions;

public static class Strategies
{
    public static IServiceCollection RegisterStrategies(this IServiceCollection services)
    => services
        .AddScoped<ICreditLimitStrategy, SmallRevenueStrategy>()
        .AddScoped<ICreditLimitStrategy, ServicesMediumRevenueStrategy>()
        .AddScoped<ICreditLimitStrategy, ProductsMediumRevenueStrategy>()
        .AddScoped<ICreditLimitStrategy, ServicesLargeRevenueStrategy>()
        .AddScoped<ICreditLimitStrategy, ProductsLargeRevenueStrategy>();
}
