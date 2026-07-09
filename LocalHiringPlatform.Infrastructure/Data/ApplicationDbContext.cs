using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.CandidateEducation;
using LocalHiringPlatform.Domain.Entities.Workshop;
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

    public DbSet<TestClass> TestClasses { get; set; }

    public DbSet<Education> Educations { get; set; }

    public DbSet<University> Universities { get; set; }

    public DbSet<Specialization> Specializations { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Specialization> CandidateEducationSpecializations { get; set; }

    public DbSet<CandidateEducation> CandidateEducations { get; set; }

    public DbSet<CourseSpecialization> CourseSpecializations { get; set; }

    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<State>().HasOne<Country>().WithMany();

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
                }).IsUnique();

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

        
        modelBuilder.Entity<Course>(entity =>
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
        });

        modelBuilder.Entity<CandidateEducationSpecialization>(entity =>
        {
            entity.HasKey(x => x.CandidateEducationSpecializationId);

            entity.HasOne(x => x.CandidateEducation)
                .WithMany(x => x.CandidateEducationSpecializations)
                .HasForeignKey(x => x.CandidateEducationEntityId)
                .HasPrincipalKey(x => x.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.CourseSpecialization)
                .WithMany(x => x.CandidateEducationSpecializations)
                .HasForeignKey(x => x.CourseSpecializationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(x => new
            {
                x.CandidateEducationEntityId,
                x.CourseSpecializationId
            })
            .IsUnique();
        });

        modelBuilder.Entity<CandidateEducation>(entity =>
        {
            entity.HasOne(x => x.CandidateProfile)
                .WithMany(x => x.CandidateEducations)
                .HasForeignKey(x => x.CandidateProfileId)
                .HasPrincipalKey(x => x.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Education)
                .WithMany()
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(x => x.University)
                .WithMany()
                .HasForeignKey(x => x.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(x => x.CandidateEducationSpecializations)
                .WithOne(x => x.CandidateEducation)
                .HasForeignKey(x => x.CandidateEducationEntityId)
                .HasPrincipalKey(x => x.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}