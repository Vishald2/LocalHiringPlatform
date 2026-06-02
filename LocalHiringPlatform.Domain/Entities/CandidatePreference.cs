using LocalHiringPlatform.Domain.Entities;

public class CandidatePreference : BaseEntity
{
    public Guid CandidateProfileId { get; set; }

    public string PreferredLocation { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public bool WorkFromHomeAllowed { get; set; }

    public bool ImmediateJoiner { get; set; }
}