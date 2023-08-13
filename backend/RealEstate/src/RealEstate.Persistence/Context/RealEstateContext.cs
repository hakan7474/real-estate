using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.Context;
using RealEstate.Core.Domain.Entities;
using RealEstate.Core.Domain.EntityExtensions;
using RealEstate.Domain.Entities;
using RealEstate.Persistence.Extensions;

namespace RealEstate.Persistence.Context;

public class RealEstateContext : DbContext, IRealEstateContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Types> Types { get; set; }
    public DbSet<TypeDetail> TypeDetails { get; set; }
    public DbSet<Estate> Estates { get; set; }

    public override int SaveChanges()
    {
        TrackChanges();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        TrackChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.CommonPropertyManyTableConvention();

        modelBuilder.Entity<Estate>()
            .OwnsOne(e => e.Contact);

        base.OnModelCreating(modelBuilder);
    }

    private void TrackChanges()
    {
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if ((entry.Entity is not IAuditableEntity auditable)) continue;

            if (auditable == null) throw new ArgumentNullException(nameof(auditable));

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.ApplyConceptsForAddedEntity();
                    break;
                case EntityState.Modified:
                    entry.ApplyConceptsForUpdatedEntity();
                    break;
                case EntityState.Deleted:
                    entry.ApplyConceptsForDeletedEntity();
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("EntityState type is not defined.");
            }
        }
    }
}