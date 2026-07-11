using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
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

    public DbSet<CandidateCourseSpecialization> CandidateCourseSpecializations { get; set; }

    public DbSet<CandidateEducation> CandidateEducations { get; set; }

    public DbSet<CourseSpecialization> CourseSpecializations { get; set; }

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
            .HasForeignKey(x => x.EmployerProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.Job)
            .WithMany(x => x.JobApplications)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.CandidateProfile)
            .WithMany(x => x.JobApplications)
            .HasForeignKey(x => x.CandidateProfileId)
            .OnDelete(DeleteBehavior.Cascade);

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

        modelBuilder.Entity<SavedJob>(entity =>
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
        });

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

        modelBuilder.Entity<CandidateEducation>(entity =>
        {
            entity.HasOne(x => x.CandidateProfile)
                .WithMany()
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
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(x => x.EducationId);

            entity.HasIndex(x => x.Code)
                .IsUnique();

            entity.HasMany(x => x.Courses)
                .WithOne(x => x.Education)
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(x => x.SpecializationId);

            entity.HasIndex(x => x.Code)
                .IsUnique();
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(x => x.UniversityId);

            entity.HasIndex(x => x.Code)
                .IsUnique();
        });

        modelBuilder.Entity<CourseSpecialization>(entity =>
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
        });

        modelBuilder.Entity<CandidateCourseSpecialization>(entity =>
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
        });

    }
}