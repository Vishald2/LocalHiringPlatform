namespace LocalHiringPlatform.Domain.Entities.Experience
{
    public class IndustryType
    {
        public int IndustryTypeId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public string Description { get; set; } = string.Empty;

        public ICollection<CandidateExperience> CandidateExperiences { get; set; } = new List<CandidateExperience>();
    }
}
