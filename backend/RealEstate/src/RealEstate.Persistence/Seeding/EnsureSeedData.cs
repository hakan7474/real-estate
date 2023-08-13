using Microsoft.VisualBasic;
using RealEstate.Domain.Entities;
using RealEstate.Domain.SeedData;
using RealEstate.Persistence.Context;

namespace RealEstate.Persistence.Seeding;

public static class EnsureSeedData
{
    public static async Task TypeAndTypeDetailSeedAsync(RealEstateContext context)
    {
        var typeData = context.Types.ToList();

        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var types = new List<Types>();
            //EstateType
            SaveEstateType(typeData, types);

            if (types.Count > 0)
            {
                await context.Types.AddRangeAsync(types);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    private static void SaveEstateType(IEnumerable<Types> dbType, List<Types> types)
    {
        var check = dbType.Any(x => x.TypeCode == EstateType.EstateTypeCode);

        if (!check)
        {
            types.Add(EstateType.EstateTypeData());
        }
    }
}