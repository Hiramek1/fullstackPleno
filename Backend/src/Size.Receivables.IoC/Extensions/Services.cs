using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.Application.Interfaces;
using Size.Receivables.Application.Services;
using Size.Receivables.CrossCutting.Helpers;

namespace Size.Receivables.IoC.Extensions;

public static class Services
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    => services
        .AddScoped<ICartService, CartService>();
}
