using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.EfCore.Configurations;

public class DomainRootEntityConfig<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : DomainRootEntity<TKey> where TKey : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}