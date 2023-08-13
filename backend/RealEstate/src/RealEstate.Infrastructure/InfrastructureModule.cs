using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Infrastructure;

public static class InfrastructureModule
{
    /// <summary>
    /// Genellikle sisteme eklenecek dış/external yapılanmalar bu katmanda dahil edilir.
    /// Örneğin, Email/Sms, Notification, Payment gibi
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}