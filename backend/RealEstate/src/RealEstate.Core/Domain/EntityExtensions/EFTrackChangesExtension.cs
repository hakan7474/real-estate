using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Domain.EntityExtensions;

public static class EFTrackChangesExtension
{
    public  static void ApplyConceptsForAddedEntity(this EntityEntry entry)
    {
        if (entry.Entity is not ICreateAuditableEntity && entry.Entity is not ISoftDelete)
        {
            return;
        }

        if (entry.Entity is ICreateAuditableEntity)
        {
			entry.Entity.As<ICreateAuditableEntity>().CreatedDate = DateTime.Now;
        }

        if (entry.Entity is ISoftDelete)
            entry.Entity.As<ISoftDelete>().IsDeleted = false;
    }

    public static void ApplyConceptsForUpdatedEntity(this EntityEntry entry)
    {
        if (entry.Entity is not IUpdateAuditableEntity && entry.Entity is not ISoftDelete)
        {
            return;
        }

        if (entry.Entity is ISoftDelete)
            entry.Entity.As<ISoftDelete>().IsDeleted = false;
        
        if (entry.Entity is IUpdateAuditableEntity)
        {
            entry.Entity.As<IUpdateAuditableEntity>().UpdatedDate = DateTime.Now;
        }
    }

    public static void ApplyConceptsForDeletedEntity(this EntityEntry entry)
    {
        if (entry.Entity is not IDeleteAuditableEntity && entry.Entity is not ISoftDelete)
        {
            return;
        }

        entry.Reload();
        if (entry.Entity is IDeleteAuditableEntity)
        {
            entry.Entity.As<IDeleteAuditableEntity>().IsActive = false;
            entry.Entity.As<IDeleteAuditableEntity>().UpdatedDate = DateTime.Now;
        }

        if (entry.Entity is ISoftDelete)
            entry.Entity.As<ISoftDelete>().IsDeleted = true;

        entry.State = EntityState.Modified;
    }
}