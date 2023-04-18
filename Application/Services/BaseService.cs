using Application.Interfaces;
using Common.Constants;
using Data.Enums;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Services
{
    public class BaseService<TEntity, TId> : ICrudService<TEntity, TId> where TEntity : class, IEntity<TId> where TId : struct
    {
        private readonly IRepository<TEntity, TId> _repository;
        public BaseService(IRepository<TEntity, TId> repository)
        {
            Log.Verbose(Messages.CreatedInstanceOf + nameof(IRepository<TEntity, TId>));
            _repository = repository;
        }

        public virtual async Task<TId> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (await _repository.CheckExistsAsync(entity.Id, cancellationToken))
                throw new ArgumentException(Messages.IdInUse);

            var repositoryResult = await _repository.CreateAsync(entity, cancellationToken);

            if (repositoryResult.RepositoryActionResult is not RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return repositoryResult.CreatedEntityId;
        }

        public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (await _repository.CheckExistsAsync(id, cancellationToken))
                throw new ArgumentException(Messages.EntityDoesNotExist);

            var repositoryResult = await _repository.DeleteAsync(id, cancellationToken = default);

            if (repositoryResult is not RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);
        }

        public virtual async Task<TEntity?> FindAync(TId id, CancellationToken cancellationToken = default)
        {
            if (await _repository.CheckExistsAsync(id, cancellationToken))
                throw new ArgumentException(Messages.EntityDoesNotExist);

            var repositoryResult = await _repository.FindAsync(id, cancellationToken);

            return repositoryResult;
        }

        public virtual async Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            var repositoryResult = await _repository.Query().ToListAsync(cancellationToken);

            return repositoryResult;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (await _repository.CheckExistsAsync(entity.Id, cancellationToken))
                throw new ArgumentException(Messages.EntityDoesNotExist);

            var repositoryResult = await _repository.UpdateAsync(entity, cancellationToken);

            if (repositoryResult is not RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);
        }
    }
}
