using ERP.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using MT.Innovation.Shared.Infrastructure;

namespace Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void PreserveAuditFields(this DbContext context, ApplicationInfo applicationInfo)
        {
            var entities = context.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            var now = DateTime.Now;
            var username = applicationInfo.CurrentUserName;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is EntityBase entityBase)
                    {
                        entityBase = (EntityBase)entity.Entity;
                        entityBase.CreatedAt = now;
                        entityBase.CreatedBy = username;
                    }
                }
                if (entity.State == EntityState.Modified)
                {
                    var auditableEntityBase = (BaseAuditableEntity)entity.Entity;
                    auditableEntityBase.UpdatedAt = now;
                    auditableEntityBase.UpdatedBy = username;
                }
                if (entity.State == EntityState.Deleted && entity.Entity is BaseAuditableEntity)
                {
                    var auditableEntityBase = (BaseAuditableEntity)entity.Entity;
                    entity.State = EntityState.Modified;
                    auditableEntityBase.UpdatedAt = now;
                    auditableEntityBase.UpdatedBy = username;
                    auditableEntityBase.IsDeleted = true;
                }
            }
        }
    }
}
