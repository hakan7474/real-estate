using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Domain.Entities;
using RealEstate.Core.Domain.EntityExtensions;

namespace RealEstate.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void CommonPropertyManyTableConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => typeof(AAuditableEntity<Guid>).IsAssignableFrom(e.ClrType)))
        {
            entityType.SetTableName(entityType.DisplayName());

            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                modelBuilder.SetSoftDeleteFilter(entityType.ClrType);

            modelBuilder.HasSequence<long>($"{entityType.DisplayName()}_SysCode_seq")
                .IncrementsBy(1)
                .StartsAt(1)
                .HasMin(1)
                .HasMax(long.MaxValue);

            modelBuilder.Entity(
                entityType.Name,
                x =>
                {
                    x.HasKey("Id");

                    x.Property<Guid>("Id")
                        .HasDefaultValueSql("uuid_generate_v4()")
                        .ValueGeneratedOnAdd();

                    x.Property<DateTime?>("CreatedDate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP")
                        .IsRequired(false);

                    x.Property<bool>("IsActive")
                        .HasDefaultValue(true);

                    x.Property<bool>("IsDeleted")
                        .HasDefaultValue(false);

                    x.Property<string>("SysCode")
                        .HasDefaultValueSql($"'{entityType.GetDefaultTableName()?[..3].ToUpperInvariant() ?? "REAL"}-'::text || nextval('\"{entityType.DisplayName()}_SysCode_seq\"')")
                        .ValueGeneratedOnAdd();
                });
        }
    }
}