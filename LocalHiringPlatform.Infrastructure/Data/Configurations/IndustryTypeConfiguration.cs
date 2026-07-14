using System;
using System.Collections.Generic;
using System.Linq;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class IndustryTypeConfiguration
                    : IEntityTypeConfiguration<IndustryType>
    {
        public void Configure(
            EntityTypeBuilder<IndustryType> entity)
        {
            entity.ToTable("IndustryTypes");

            entity.HasMany(i => i.CandidateExperiences)
                    .WithOne(ce => ce.IndustryType)
                    .HasForeignKey(ce => ce.IndustryTypeId)
                    .OnDelete(DeleteBehavior.NoAction);

            entity.HasKey(x => x.IndustryTypeId);

            entity.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}
