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

    public DbSet<CandidateSkill> CandidateSkills => Set<CandidateSkill>();

    public DbSet<CandidateEducation> CandidateEducations => Set<CandidateEducation>();

    public DbSet<CandidateExperience> CandidateExperiences => Set<CandidateExperience>();

    public DbSet<CandidateResume> CandidateResumes => Set<CandidateResume>();

    public DbSet<Skill> Skills => Set<Skill>();
}