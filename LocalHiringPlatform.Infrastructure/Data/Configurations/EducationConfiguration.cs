using System;
using System.Collections.Generic;
using System.Linq;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class EducationConfiguration
                    : IEntityTypeConfiguration<Education>
    {
        public void Configure(
            EntityTypeBuilder<Education> entity)
        {
            entity.HasKey(x => x.EducationId);

            entity.HasIndex(x => x.Code)
                .IsUnique();

            entity.HasMany(x => x.Courses)
                .WithOne(x => x.Education)
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
