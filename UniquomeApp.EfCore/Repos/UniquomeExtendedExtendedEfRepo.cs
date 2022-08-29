using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using UniquomeApp.Domain.Base;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.EfCore.Repos;

public class UniquomeExtendedExtendedEfRepo<TEntity> :
    RepositoryBase<TEntity>,
    IUniquomeExtendedRepository<TEntity> where TEntity : DomainRootEntity<long>
{
    protected readonly UniquomeDbContext DbContext;

    public UniquomeExtendedExtendedEfRepo(UniquomeDbContext context) : base(context)
    {
        DbContext = context;
    }

    public virtual async Task<int> UpdateMultipleAsync(IList<TEntity> entitiesToUpdate, int batchSize = 1, CancellationToken cancellationToken = default)
    {
        var actualBatchSize = entitiesToUpdate.Count;
        if (batchSize > -1)
            actualBatchSize = batchSize;
        var recCount = 0;
        foreach (var entityToUpdate in entitiesToUpdate)
        {
            try
            {
                DbContext.Entry(entityToUpdate).State = EntityState.Modified;
                recCount++;
                if (recCount != actualBatchSize) continue;
                await DbContext.SaveChangesAsync(cancellationToken);
                recCount = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        if (recCount > 0)
            await DbContext.SaveChangesAsync(cancellationToken);

        return recCount;
    }


    public virtual async Task<int> CreateMultipleAsync(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default)
    {
        //TODO: Maybe Deprecate. Check if there are any benefits over BulkInsert
        var actualBatchSize = entities.Count;
        if (batchSize > -1)
            actualBatchSize = batchSize;
        var recCount = 0;
        var totalRecs = 0;
        foreach (var entity in entities)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            recCount++;
            totalRecs++;
            if (recCount != actualBatchSize) continue;
            await DbContext.SaveChangesAsync(cancellationToken);
            recCount = 0;
        }

        if (recCount > 0)
            await DbContext.SaveChangesAsync(cancellationToken);

        return totalRecs;
    }

    public async Task<int> BulkInsert(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default)
    {
        // await using (var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken))
        // {
        //     var bulkConfig = new BulkConfig();
        //     //
        //     // IList<TEntity> batchCollection = new List<TEntity>();
        //     // foreach (var entity in entities)
        //     // {
        //     //     batchCollection.Add(entity);
        //     //     if (batchCollection.Count != batchSize) continue;
        //     //     await DbContext.BulkInsertAsync(batchCollection, bulkConfig, null, null, cancellationToken);
        //     //     batchCollection.Clear();
        //     // }
        //     // if (batchCollection.Any())
        //     //     await DbContext.BulkInsertAsync(batchCollection, bulkConfig, null, null, cancellationToken);
        //     if (entities.Any())
        //         await DbContext.BulkInsertAsync(entities, bulkConfig, null, null, cancellationToken);
        //
        //     await transaction.CommitAsync(cancellationToken);
        // }
        var bulkConfig = new BulkConfig();
        await DbContext.BulkInsertAsync(entities, bulkConfig, null, null, cancellationToken);

        return entities.Count;
    }

    public async Task<TEntity?> GetAsync(ISpecification<TEntity?> spec, CancellationToken cancellationToken = default)
    {
        return await base.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
            await base.DeleteAsync(entity, cancellationToken);
        return true;
    }

    public IQueryable<TEntity> GetQueryable(ISpecification<TEntity> spec, bool readOnly = true, CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec, readOnly);
    }

    public async Task<int> CreateMultipleByRemovingEntitiesAsync(IList<TEntity> entities, int batchSize = 1, CancellationToken cancellationToken = default)
    {
        var actualBatchSize = entities.Count;
        if (batchSize > -1)
            actualBatchSize = batchSize;
        var recCount = 0;
        var totalRecs = entities.Count;

        foreach (var entity in entities)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            recCount++;
            totalRecs++;
            if (recCount != actualBatchSize) continue;
            var entriesToRemove = DbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            await DbContext.SaveChangesAsync(cancellationToken);
            recCount = 0;
            foreach (var e in entriesToRemove)
                e.State = EntityState.Detached;
        }

        if (recCount <= 0) return totalRecs;
        var entriesToRemove2 = DbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
        await DbContext.SaveChangesAsync(cancellationToken);
        foreach (var e in entriesToRemove2)
            e.State = EntityState.Detached;

        await DbContext.DisposeAsync();
        return totalRecs;
    }
}