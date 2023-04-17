using Common.Enums;

namespace Data.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        IQueryable<TEntity> Query();
        Task<bool> CheckExistsAsync(TId id, CancellationToken cancellationToken);
        Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken);
        Task<(RepositoryAction RepositoryActionResult, TId CreatedEntityId)> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<RepositoryAction> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<RepositoryAction> DeleteAsync(TId id, CancellationToken cancellationToken);
    }
}
