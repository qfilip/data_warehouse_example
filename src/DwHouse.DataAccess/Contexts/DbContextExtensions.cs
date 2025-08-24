using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Enums;
using DwHouse.DataAccess.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace DwHouse.DataAccess.Contexts;
public static class DbContextExtensions
{
    internal static void AddGlobalQueryFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if(entityType.ClrType.IsAssignableFrom(typeof(ISoftDeletable)))
            {
                var param = Expression.Parameter(entityType.ClrType, "i");
                var prop = Expression.PropertyOrField(param, nameof(ISoftDeletable.EntityStatus));
                var expression = Expression.NotEqual(prop, Expression.Constant(eEntityStatus.Deleted));

                entityType.SetQueryFilter(Expression.Lambda(expression, param));
            }
        }
    }

    internal static void AuditEntries(DbContext context)
    {
        var entries = context.ChangeTracker.Entries();

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            var entryKey = entry.Entity as IId<Guid>;
            var entryAudit = entry.Entity as IAuditedEntity<Audit>;

            if (entryKey == null || entryAudit == null)
                continue;

            if (entry.State == EntityState.Added)
            {
                entryKey.Id = entryKey.Id == Guid.Empty ? Guid.NewGuid() : entryKey.Id;

                entryAudit.AuditRecord.CreatedAt = now;
                entryAudit.AuditRecord.ModifiedAt = now;

            }
            else if (entry.State == EntityState.Modified)
            {
                entryAudit.AuditRecord.ModifiedAt = now;
            }
        }
    }

    internal static void ConfigureEntity<TEntity>(
        EntityTypeBuilder<TEntity> mb,
        Action<EntityTypeBuilder<TEntity>>? customConfiguration = null)
        where TEntity : class
    {
        ConfigureEntity<TEntity, Guid, Audit>(mb, customConfiguration);
    }

    internal static void ConfigureEntity<TEntity, TPKey, TAudit>(
        EntityTypeBuilder<TEntity> etb,
        Action<EntityTypeBuilder<TEntity>>? customConfiguration = null)
        where TEntity : class
        where TPKey : struct
        where TAudit : class
    {
        var type = typeof(TEntity);
        
        etb.ToTable(type.Name);

        if (typeof(IId<TPKey>).IsAssignableFrom(type))
            etb.HasKey(x => ((IId<TPKey>)x).Id);

        if (typeof(ISoftDeletable).IsAssignableFrom(type))
            etb.HasQueryFilter(x => ((ISoftDeletable)x).EntityStatus == eEntityStatus.Active);

        if (typeof(IAuditedEntity<TAudit>).IsAssignableFrom(type))
            etb.OwnsOne(x => ((IAuditedEntity<TAudit>)x).AuditRecord);

        customConfiguration?.Invoke(etb);
    }
}

