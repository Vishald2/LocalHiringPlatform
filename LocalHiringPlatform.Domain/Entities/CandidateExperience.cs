namespace LocalHiringPlatform.Domain.Entities;
public class CandidateExperience
{
    public Guid Id { get; set; }

    public Guid CandidateProfileId { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string Designation { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsCurrentCompany { get; set; }

    public string JobDescription { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}