using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class ApplicationUserEfConfig : SimpleEntityConfig<ApplicationUser>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.Email).IsRequired().HasMaxLength(Sizes.Email);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(Sizes.Name);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(Sizes.Name);
        builder.Property(x => x.Institution).IsRequired(false).HasMaxLength(Sizes.Name);
        builder.Property(x => x.Position).IsRequired(false).HasMaxLength(Sizes.Name);
        builder.Property(x => x.Country).IsRequired(false).HasMaxLength(Sizes.Name);
        
    }
}