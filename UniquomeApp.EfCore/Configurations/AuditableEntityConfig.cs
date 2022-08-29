using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain.Base;

namespace UniquomeApp.EfCore.Configurations;

public class AuditableEntityConfig<TEntity, TKey> : DomainRootEntityConfig<TEntity, TKey> where TEntity : AuditableEntity<TKey> where TKey : struct
{
    private const int UserSize = 100;
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.CreatedBy).HasMaxLength(UserSize);
        builder.Property(x => x.LastModifiedAt);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(UserSize);
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy).HasMaxLength(UserSize);
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}