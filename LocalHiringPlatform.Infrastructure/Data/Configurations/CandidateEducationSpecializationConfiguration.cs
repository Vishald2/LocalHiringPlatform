using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class CandidateEducationSpecializationConfiguration
                    : IEntityTypeConfiguration<CandidateEducationSpecialization>
    {
        public void Configure(
            EntityTypeBuilder<CandidateEducationSpecialization> entity)
        {
            entity.HasKey(x => x.CandidateEducationSpecializationId);

            entity.HasOne(x => x.CandidateEducation)
                .WithMany(x => x.CandidateCourseSpecializations)
                .HasForeignKey(x => x.CandidateEducationEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Specialization)
                .WithMany()
                .HasForeignKey(x => x.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(x => new
            {
                x.CandidateEducationEntityId,
                x.SpecializationId
            }).IsUnique();
        }
    }
}