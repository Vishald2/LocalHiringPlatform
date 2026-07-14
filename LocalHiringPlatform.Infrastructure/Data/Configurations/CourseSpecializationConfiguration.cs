using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{

    public class CourseSpecializationConfiguration
                    : IEntityTypeConfiguration<CourseSpecialization>
    {
        public void Configure(
            EntityTypeBuilder<CourseSpecialization> entity)
        {
            entity.HasKey(x => x.CourseSpecializationId);

            entity.HasOne(x => x.Course)
                .WithMany(x => x.CourseSpecializations)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(x => x.Specialization)
                        .WithMany(x => x.CourseSpecializations)
                        .HasForeignKey(x => x.SpecializationId)
                        .OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(x => new
            {
                x.CourseId,
                x.SpecializationId
            }).IsUnique();
        }
    }
}




