namespace Application.Interfaces
{
    public interface ICrudService<TEntity, TId> where TEntity : class where TId : struct
    {
        Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity?> FindAync(TId id, CancellationToken cancellationToken);
        Task<TId> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TId id, CancellationToken cancellationToken);
    }
}
