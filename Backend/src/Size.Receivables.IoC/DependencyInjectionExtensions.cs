using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.IoC.Extensions;

namespace Size.Receivables.IoC;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection DependencyRegistry(this IServiceCollection services)
    {
        services
            .RegisterHelpers()
            .RegisterRepositories()
            .RegisterServices()
            .RegisterStrategies()
            .RegisterUseCases();

        return services;
    }
}
