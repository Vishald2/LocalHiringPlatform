namespace LocalHiringPlatform.Api.DTOs
{
    public class SaveCandidateSkillsRequestDto
    {
        public List<Guid> SkillIds { get; set; } = new();
    }
}
