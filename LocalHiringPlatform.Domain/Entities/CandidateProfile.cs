namespace LocalHiringPlatform.Domain.Entities;

public class CandidateProfile
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string ProfileSummary { get; set; } = string.Empty;

    public decimal? CurrentSalary { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public decimal TotalExperienceYears { get; set; }

    public string ResumeUrl { get; set; } = string.Empty;

    public int ProfileCompletionPercentage { get; set; }

    public bool IsOpenToWork { get; set; } = true;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}