namespace LocalHiringPlatform.Api.DTOs
{
    public class CandidateSearchRequestDto
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        public Guid? SkillId { get; set; }
    }
}
