using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Features.Estate.Rules;
using System.Reflection;

namespace RealEstate.Application;

public static class ApplicationModule
{
    /// <summary>
    /// Bu katman, Domain katmanı ile uygulamanın iş/business/service katmanı arasında bir soyutlama katmanıdır. Repository olsun, service olsun tüm arayüzler burada tanımlanır. Amaç veri erişiminde Gevşek Bağlı(Loose Coupling) bir yaklaşım sergilemektir. Domain katmanını referans eder.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly)
            .AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(assembly);
            }).AddFluentValidation(c => c.RegisterValidators(assembly));


        services.AddScoped<EstateBusinessRule>();

        return services;
    }
    public static FluentValidationMvcConfiguration RegisterValidators(this FluentValidationMvcConfiguration conf, Assembly assembly)
    {
        conf.RegisterValidatorsFromAssembly(assembly);

        return conf;
    }
}