namespace Application.Interfaces
{
    public interface ICrudService<TEntity, TId> where TEntity : class where TId : struct
    {
        IQueryable<TEntity> Query();
        Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task<TEntity?> FindAync(TId id, CancellationToken cancellationToken = default);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}
