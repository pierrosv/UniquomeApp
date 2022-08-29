using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;

namespace UniquomeApp.EfCore.Configurations;

public class UniquomeProteinEfConfig : SimpleEntityConfig<UniquomeProtein>
{
    public override void Configure(EntityTypeBuilder<UniquomeProtein> builder)
    {
        builder.HasOne(x => x.ForUniquome).WithMany(x => x.Proteins).HasForeignKey(x => x.ForUniquomeId).IsRequired();
        builder.HasOne(x => x.ForProtein).WithMany().HasForeignKey(x => x.ForProteinId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasMany(x => x.Peptides).WithOne(x => x.InUniquomeProtein).HasForeignKey(x => x.InUniquomeProteinId).OnDelete(DeleteBehavior.Cascade);
    }
}