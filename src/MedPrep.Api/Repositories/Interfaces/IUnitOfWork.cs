namespace MedPrep.Api.Repositories.Interfaces;

using System.Data;

public interface IUnitOfWork
{
    Task SaveChangesAsync(bool hardDelete = false, CancellationToken cancellationToken = default);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
