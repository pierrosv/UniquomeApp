using Ardalis.Specification;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain.Base;

public interface IUniquomeExtendedRepository<TEntity> :  
    IAsyncRepository<TEntity, long>
    where TEntity : DomainRootEntity<long>
{
    IQueryable<TEntity> GetQueryable(ISpecification<TEntity> spec, bool readOnly = true, CancellationToken cancellationToken = default);
    Task<int> CreateMultipleByRemovingEntitiesAsync(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default);
}