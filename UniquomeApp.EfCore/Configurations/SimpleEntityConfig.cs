using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.EfCore.Configurations;

public class SimpleEntityConfig<TEntity> : DomainRootEntityConfig<TEntity, long> where TEntity : SimpleEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
    }
}