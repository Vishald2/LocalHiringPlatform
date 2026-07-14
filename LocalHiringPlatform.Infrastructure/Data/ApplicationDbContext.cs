using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using CandidateExperience = LocalHiringPlatform.Domain.Entities.Experience.CandidateExperience;

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

    public DbSet<CandidateEducationSpecialization> CandidateEducationSpecializations { get; set; }

    public DbSet<CandidateEducation> CandidateEducations { get; set; }

    public DbSet<CourseSpecialization> CourseSpecializations { get; set; }

    public DbSet<CandidateExperience> CandidateExperiences { get; set; }

    public DbSet<CandidateExperienceDetail> CandidateExperienceDetails { get; set; }

    public DbSet<IndustryType> IndustryTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
                    typeof(ApplicationDbContext).Assembly);

        SeedMasterData(modelBuilder);

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

        modelBuilder.Entity<AiAnalysis>()
        .HasOne(
            x => x.JobApplication)
        .WithOne(
            x => x.AiAnalysis)
        .HasForeignKey<AiAnalysis>(
            x => x.JobApplicationId);

        
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

        modelBuilder.Entity<CandidateExperience>(entity =>
        {
            entity.HasMany(ce=> ce.ExperienceDetails)
                .WithOne(ed => ed.CandidateExperience)
                .HasForeignKey(ed => ed.CandidateExperienceId)
                .OnDelete(DeleteBehavior.Cascade);
        });


    }

    private static void SeedMasterData(
    ModelBuilder modelBuilder)
    {
        //EducationSeedData.Seed(modelBuilder);
        //CourseSeedData.Seed(modelBuilder);
        //SkillSeedData.Seed(modelBuilder);
        IndustryTypeSeedData.Seed(modelBuilder);
    }
}