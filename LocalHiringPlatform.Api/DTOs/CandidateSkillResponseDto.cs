namespace LocalHiringPlatform.Api.DTOs
{
    public class CandidateSkillResponseDto
    {
        public Guid SkillId { get; set; }

        public string SkillName { get; set; } = string.Empty;

        public string IndustryType { get; set; } = string.Empty;

        public string SkillCategory { get; set; } = string.Empty;

        public int ExperienceInMonths { get; set; }
    }
}
