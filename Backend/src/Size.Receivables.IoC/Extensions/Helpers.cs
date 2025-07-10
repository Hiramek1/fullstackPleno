using Microsoft.Extensions.DependencyInjection;
using Size.Receivables.CrossCutting.Helpers;

namespace Size.Receivables.IoC.Extensions;

public static class Helpers
{
    public static IServiceCollection RegisterHelpers(this IServiceCollection services)
    => services
        .AddScoped<IDateTimeHelper, DateTimeHelper>();
}
