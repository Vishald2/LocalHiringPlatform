using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Enums;

public class CandidateProfile : BaseEntity
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public string ProfileSummary { get; set; } = string.Empty;

    public decimal? CurrentSalary { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public decimal TotalExperienceYears { get; set; }

    public string ResumeUrl { get; set; } = string.Empty;

    public int ProfileCompletionPercentage { get; set; }

    public bool IsOpenToWork { get; set; } = true;

    public string? ResumeFileName { get; set; }
    public string? ResumeFilePath { get; set; }

    public User User { get; set; } = null!;

    /* Navigation Properties*/

    public ICollection<JobApplication>
    JobApplications { get; set; } = new List<JobApplication>();

    public ICollection<CandidateSkill> CandidateSkills { get; set; }
    = new List<CandidateSkill>();
}