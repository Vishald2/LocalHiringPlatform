namespace LocalHiringPlatform.Domain.Entities;

public class CandidateEducation
{
    public Guid Id { get; set; }

    public Guid CandidateProfileId { get; set; }

    public string Qualification { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string InstituteName { get; set; } = string.Empty;

    public int PassingYear { get; set; }

    public decimal? PercentageOrCGPA { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}