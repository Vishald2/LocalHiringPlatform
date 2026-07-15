using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class CandidateProfileConfiguration
                    : IEntityTypeConfiguration<CandidateProfile>
    {
        public void Configure(
            EntityTypeBuilder<CandidateProfile> entity)
        {

            entity.HasKey(e => e.EntityId);

            entity.HasMany(cp => cp.CandidateExperiences)
                .WithOne(ce => ce.CandidateProfile)
                .HasForeignKey(ce => ce.CandidateProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
