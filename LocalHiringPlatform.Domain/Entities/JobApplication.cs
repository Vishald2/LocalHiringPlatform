using LocalHiringPlatform.Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid JobId { get; set; }

    public Job Job { get; set; } = null!;

    public Guid CandidateProfileId { get; set; }

    public CandidateProfile CandidateProfile { get; set; } = null!;

    public DateTime AppliedOn { get; set; }

    public string Status { get; set; } = "Applied";
}