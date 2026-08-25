using ECommerceApp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerceApp.Infrastructure.Persistence.Interceptors;

public class TimestampInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateTimestamps(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateTimestamps(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateTimestamps(DbContext? context)
    {
        if (context is null) return;

        var now = DateTime.UtcNow;

        var entries = context.ChangeTracker.Entries<IHasTimeStamp>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Property(b => b.CreatedAt).IsModified = false;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;

                    foreach (var reference in entry.References)
                    {
                        if (reference.TargetEntry?.Metadata.IsOwned() == true)
                        {
                            reference.TargetEntry.State = EntityState.Unchanged;
                        }
                    }

                    entry.Entity.IsDeleted = true;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.DeletedAt = now;

                    entry.Property(b => b.CreatedAt).IsModified = false;
                    break;
            }
        }
    }
}
