using Common.Constants;
using Data.Enums;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Data.Repositories
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TId : struct
    {
        protected readonly SnouterDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(SnouterDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region Internal methods
        protected RepositoryAction SaveChanges()
        {
            return _dbContext.SaveChanges() > 0
                ? RepositoryAction.Success
                : RepositoryAction.NoChanges;
        }
        protected async Task<RepositoryAction> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0
                ? RepositoryAction.Success
                : RepositoryAction.NoChanges;
        }

        protected RepositoryAction TryRepositoryAction(Func<RepositoryAction> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                Log.Error(Messages.CaughtException, e);
                return RepositoryAction.Error;
            }
        }
        protected async Task<RepositoryAction> TryRepositoryActionAsync(Func<Task<RepositoryAction>> actionAsync)
        {
            try
            {
                return await actionAsync();
            }
            catch (Exception e)
            {
                Log.Error(Messages.CaughtException, e);
                return RepositoryAction.Error;
            }
        }
        #endregion

        public IQueryable<TEntity> Query()
        {
            return _dbSet
                .AsQueryable();
        }

        public async Task<bool> CheckExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken) is not null;
        }

        public async Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<(RepositoryAction RepositoryActionResult, TEntity CreatedEntity)> CreateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            var repositoryActionResult = await TryRepositoryActionAsync(async () =>
            {
                await _dbSet.AddAsync(entity, cancellationToken);
                return await SaveChangesAsync(cancellationToken);
            });

            return (repositoryActionResult, entity);
        }

        public Task<RepositoryAction> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return TryRepositoryActionAsync(async () =>
            {
                _dbSet.Update(entity);
                return await SaveChangesAsync(cancellationToken);
            });
        }

        public Task<RepositoryAction> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            return TryRepositoryActionAsync(async () =>
            {
                var toRemove = await FindAsync(id, cancellationToken)
                    ?? throw new ArgumentException(string.Format(Messages.EntityDoesNotExist, typeof(TEntity), id));
                _dbSet.Remove(toRemove);
                return await SaveChangesAsync(cancellationToken);
            });
        }

        public async Task BeginTransaction(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
    }
}
