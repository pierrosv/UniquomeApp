using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain.Base;

namespace UniquomeApp.EfCore.Configurations;

public class AisEntityConfig<TEntity> : AuditableEntityConfig<TEntity, long> where TEntity : AisEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
    }
}