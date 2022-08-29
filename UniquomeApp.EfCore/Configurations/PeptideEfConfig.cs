using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class PeptideEfConfig : SimpleEntityConfig<Peptide>
{
    public override void Configure(EntityTypeBuilder<Peptide> builder)
    {
        builder.HasOne(x => x.InUniquomeProtein).WithMany(x => x.Peptides).HasForeignKey(x => x.InUniquomeProteinId).IsRequired();
        builder.Property(x => x.Sequence).IsRequired().HasMaxLength(Sizes.Peptide);
        builder.Property(x => x.FirstLocation);
    }
}