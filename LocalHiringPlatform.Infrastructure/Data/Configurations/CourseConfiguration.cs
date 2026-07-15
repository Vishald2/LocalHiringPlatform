using System;
using System.Collections.Generic;
using System.Linq;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class CourseConfiguration
                    : IEntityTypeConfiguration<Course>
    {
        public void Configure(
            EntityTypeBuilder<Course> entity)
        {
            entity.HasKey(x => x.CourseId);

            entity.HasIndex(x => x.Code)
                .IsUnique();

            entity.HasOne(x => x.Education)
                .WithMany(e => e.Courses)
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(x => x.CourseSpecializations)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
