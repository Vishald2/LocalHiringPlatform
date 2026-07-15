using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class CandidateEducationConfiguration
                    : IEntityTypeConfiguration<CandidateEducation>
    {
        public void Configure(
            EntityTypeBuilder<CandidateEducation> entity)
        {
            entity.HasOne(x => x.CandidateProfile)
                .WithMany(x => x.CandidateEducations)
                .HasForeignKey(x => x.CandidateProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(x => x.University)
                .WithMany()
                .HasForeignKey(x => x.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}