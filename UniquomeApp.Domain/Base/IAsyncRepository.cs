using Ardalis.Specification;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain.Base;

public interface IAsyncRepository<TEntity, TKey> : IRepositoryBase<TEntity>
    where TEntity : DomainRootEntity<TKey>
    where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<int> UpdateMultipleAsync(IList<TEntity> entitiesToUpdate, int batchSize = 1, CancellationToken cancellationToken = default);
    // Task<int> DeleteMultipleByIdAsync(IList<TKey> ids, int batchSize = 1, CancellationToken cancellationToken = default);
    Task<int> CreateMultipleAsync(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default);
    Task<int> BulkInsert(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default);

    //TODO: Review and Incorporate Specification Members (Removed due to the use of the IRepositoryBase
    // Task<IList<TEntity>> GetAllAsync(bool readOnly = true, CancellationToken cancellationToken = default); //Task<IReadOnlyList<T>> ListAllAsync();
    // Task<bool> UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken = default);
    // Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    // Task<IList<TEntity>> ListAsync(ISpecification<TEntity> spec, bool readOnly = true, CancellationToken cancellationToken = default);
    // Task<TEntity> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    // Task<int> CountAsync(ISpecification<TEntity> spec);
    //
    // IQueryable<TEntity> GetQueryable(ISpecification<TEntity> spec, bool readOnly = true);
}