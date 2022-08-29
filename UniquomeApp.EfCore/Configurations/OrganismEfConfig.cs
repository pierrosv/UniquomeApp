using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class OrganismEfConfig : SimpleEntityConfig<Organism>
{
    public override void Configure(EntityTypeBuilder<Organism> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Sizes.OrganismName);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(Sizes.Description);
    }
}