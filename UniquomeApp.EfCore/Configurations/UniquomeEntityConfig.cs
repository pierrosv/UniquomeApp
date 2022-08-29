using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain.Base;

namespace UniquomeApp.EfCore.Configurations;

public class UniquomeEntityConfig<TEntity> : AuditableEntityConfig<TEntity, long> where TEntity : UniquomeEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
    }
}