using Microsoft.Extensions.DependencyInjection;
using RealEstate.Persistence.Seeding;

namespace RealEstate.Persistence.Context;

public static class RealEstateSeed
{
    public static async Task ApplyDbMigrationsWithDataSeedAsync(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var services = serviceScope.ServiceProvider;
        await SeedAsync(services);
    }

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RealEstateContext>();
        
        //Default migrationları atar.
        await EnsureSeedData.TypeAndTypeDetailSeedAsync(context);
       
    }
}