﻿using Application.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Data.Enums;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class BaseService<TEntity, TId> : IService<TEntity, TId> where TEntity : class, IEntity<TId> where TId : struct
    {
        protected readonly IRepository<TEntity, TId> _repository;
        public BaseService(IRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public IQueryable<TEntity> Query()
        {
            return _repository
                .Query()
                .AsNoTracking();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new BadRequestException(Messages.MissingInformation);

            if (await _repository.CheckExistsAsync(entity.Id, cancellationToken))
                throw new BadRequestException(Messages.IdInUse);

            var repositoryResult = await _repository.CreateAsync(entity, cancellationToken);

            if (repositoryResult.RepositoryActionResult is not RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);

            return repositoryResult.CreatedEntity;
        }

        public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (!await _repository.CheckExistsAsync(id, cancellationToken))
                throw new BadRequestException(Messages.MissingInformation);

            var repositoryResult = await _repository.DeleteAsync(id, cancellationToken);

            if (repositoryResult is not RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);
        }

        public virtual async Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default)
        {
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
                throw new BadRequestException(Messages.MissingInformation);

            if (!await _repository.CheckExistsAsync(entity.Id, cancellationToken))
                throw new NotFoundException(Messages.EntityDoesNotExist);

            var repositoryResult = await _repository.UpdateAsync(entity, cancellationToken);

            if (repositoryResult is not RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);
        }
    }
}
