namespace MedPrep.Api.Repositories;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MedPrep.Api.Context;
using MedPrep.Api.Models.Common;
using MedPrep.Api.Repositories.Common;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork(MedPrepContext context) : IUnitOfWork, IDisposable
{
    private readonly MedPrepContext context = context;

    public void BeginTransaction() => this.context.Database.BeginTransaction();

    public void CommitTransaction() => this.context.Database.CommitTransaction();

    public void RollbackTransaction() => this.context.Database.RollbackTransaction();

    public Task SaveChangesAsync(
        bool hardDelete = false,
        CancellationToken cancellationToken = default
    )
    {
        this.AuditEntityIntercept();
        if (!hardDelete)
        {
            this.SoftDeleteIntercept();
        }
        return this.context.SaveChangesAsync(cancellationToken);
    }

    private void SoftDeleteIntercept()
    {
        var entries = this
            .context.ChangeTracker.Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.IsDeleted = true;
            softDeletable.Entity.DeletedAt = DateTime.UtcNow;
        }
    }

    private void AuditEntityIntercept()
    {
        var entries = this
            .context.ChangeTracker.Entries()
            .Where(e =>
                e.Entity is IBaseEntity
                && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entityEntry in entries)
        {
            ((IBaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((IBaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }
    }

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
}
