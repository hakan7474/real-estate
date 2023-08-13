using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RealEstate.Application.Interfaces.Context;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core.GenericUnitOfWork;
using RealEstate.Persistence.Context;
using RealEstate.Persistence.Repositories;

namespace RealEstate.Persistence;

public static class PersistenceModule
{
    /// <summary>
    /// DbContext, migration ve veritabanı konfigürasyon işlemleri bu katmanda gerçekleştirilir. Ayrıca Application katmanındaki interface’ler burada implemente edilir.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        #region DbContext
        services.AddDbContext<RealEstateContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("RealEstateContext"));
            x.UseLazyLoadingProxies(false);
            x.EnableSensitiveDataLogging(false);
        });
        services.TryAddScoped<DbContext, RealEstateContext>();
        services.AddScoped<IRealEstateContext, RealEstateContext>();
        #endregion

        #region Uow
        services.TryAddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<DbContext>));
        #endregion

        #region Repository

        services.AddScoped<ITypeRepository,TypeRepository>();
        services.AddScoped<ITypeDetailRepository,TypeDetailRepository>();
        services.AddScoped<IEstateRepository, EstateRepository>();

        #endregion

        #region AutoMigration

        var serviceProvider = services.BuildServiceProvider();

        if (serviceProvider != null)
        {
            using var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            Console.WriteLine("Service scope created");
            var context = serviceScope.ServiceProvider.GetRequiredService<RealEstateContext>();
            Console.WriteLine("Context received");
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            Console.WriteLine("migration succeed");

        }

        #endregion


        return services;
    }
}