using System;
using System.Collections.Generic;
using System.Linq;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalHiringPlatform.Infrastructure.Data.Configurations
{
    public class SavedJobConfiguration
                    : IEntityTypeConfiguration<SavedJob>
    {
        public void Configure(
            EntityTypeBuilder<SavedJob> entity)
        {
            entity.HasIndex(x => new
            {
                x.UserId,
                x.JobId
            }).IsUnique();

            entity.HasOne(x => x.User)
                .WithMany(x => x.SavedJobs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(x => x.Job)
                .WithMany()
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
