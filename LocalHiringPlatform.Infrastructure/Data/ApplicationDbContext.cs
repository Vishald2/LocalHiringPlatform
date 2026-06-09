using Azure.Core.Pipeline;
using LocalHiringPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();

    public DbSet<EmployerProfile> EmployerProfiles => Set<EmployerProfile>();

    public DbSet<Skill> Skills { get; set; }

    public DbSet<Job> Jobs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.MobileNumber)
            .IsUnique();

        modelBuilder.Entity<Job>()
            .HasOne(x => x.EmployerProfile)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.EmployerProfileId);
    }
}