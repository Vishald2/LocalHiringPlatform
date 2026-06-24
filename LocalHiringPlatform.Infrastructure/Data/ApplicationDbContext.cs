using LocalHiringPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();
    public DbSet<EmployerProfile> EmployerProfiles => Set<EmployerProfile>();
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CandidateSkill> CandidateSkills {get; set;}
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<SavedJob> SavedJobs { get; set; }

    public DbSet<AiAnalysis> AiAnalyses {get; set;}

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

        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.Job)
            .WithMany(x => x.JobApplications)
            .HasForeignKey(x => x.JobId);

        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.CandidateProfile)
            .WithMany(x => x.JobApplications)
            .HasForeignKey(x => x.CandidateProfileId);

        modelBuilder.Entity<JobApplication>()
            .HasIndex(x =>
                new
                {
                    x.JobId,
                    x.CandidateProfileId
                })
            .IsUnique();

        modelBuilder.Entity<CandidateSkill>()
            .HasOne(x => x.CandidateProfile)
            .WithMany(x => x.CandidateSkills)
            .HasForeignKey(x => x.CandidateProfileId);

        modelBuilder.Entity<CandidateSkill>()
            .HasOne(x => x.Skill)
            .WithMany()
            .HasForeignKey(x => x.SkillId);

        modelBuilder.Entity<Skill>()
            .HasIndex(x => new
            {
                x.SkillName,
                x.SkillCategory
            }).IsUnique();

        modelBuilder.Entity<SavedJob>().HasIndex(
            x => new
            {
                x.UserId,
                x.JobId
            })
            .IsUnique();

        modelBuilder.Entity<AiAnalysis>()
        .HasOne(
            x => x.JobApplication)
        .WithOne(
            x => x.AiAnalysis)
        .HasForeignKey<AiAnalysis>(
            x => x.JobApplicationId);
    }
}