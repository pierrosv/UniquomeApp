using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class ProteinEfConfig : SimpleEntityConfig<Protein>
{
    public override void Configure(EntityTypeBuilder<Protein> builder)
    {
        builder.HasOne(x => x.InProteome).WithMany(x => x.Proteins).HasForeignKey(x => x.InProteomeId).IsRequired();
        builder.Property(x => x.Code).IsRequired().HasMaxLength(Sizes.ProteinCode);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Sizes.Name);
        builder.Property(x => x.Sequence).IsRequired().HasMaxLength(Sizes.ProteinSequence);
    }
}