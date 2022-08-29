using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class NewsletterRegistrationEfConfig : SimpleEntityConfig<NewsletterRegistration>
{
    public override void Configure(EntityTypeBuilder<NewsletterRegistration> builder)
    {
        builder.Property(x => x.Email).IsRequired().HasMaxLength(Sizes.Email);
        builder.Property(x => x.AcceptedDate);
        builder.Property(x => x.RemovedDate);
    }
}