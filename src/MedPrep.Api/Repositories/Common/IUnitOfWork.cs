namespace MedPrep.Api.Repositories.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(bool hardDelete = false, CancellationToken cancellationToken = default);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
