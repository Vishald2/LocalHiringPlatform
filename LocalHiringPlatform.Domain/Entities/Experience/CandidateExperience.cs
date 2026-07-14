namespace LocalHiringPlatform.Domain.Entities.Experience
{
    public class CandidateExperience : BaseEntity
    {
        public Guid CandidateProfileId { get; set; }

        public CandidateProfile CandidateProfile { get; set; } = null!;

        // Employment

        public string CompanyName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public int IndustryTypeId { get; set; }

        public IndustryType? IndustryType { get; set; }

        // Location

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        // Duration

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool IsCurrentCompany { get; set; }

        // Free Text

        public string Summary { get; set; } = string.Empty;

        // Navigation

        public ICollection<CandidateExperienceDetail>? ExperienceDetails {  get; set; }
        
    }
}
