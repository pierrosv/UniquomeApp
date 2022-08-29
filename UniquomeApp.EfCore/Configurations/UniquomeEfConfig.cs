﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore.Configurations;

public class UniquomeEfConfig : SimpleEntityConfig<Uniquome>
{
    public override void Configure(EntityTypeBuilder<Uniquome> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Sizes.Name);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(Sizes.Description);
        builder.Property(x => x.Version).IsRequired().HasMaxLength(Sizes.Version);
        builder.HasMany(x => x.Proteins).WithOne(x => x.ForUniquome).HasForeignKey(x => x.ForUniquomeId).OnDelete(DeleteBehavior.Cascade);
    }
}