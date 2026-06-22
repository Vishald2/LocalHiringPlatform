using LocalHiringPlatform.Domain.Enums;

namespace LocalHiringPlatform.Api.DTOs
{
    public class SkillResponseDto
    {
        public Guid EntityId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public SkillCategory SkillCategory { get; set; }
        public bool IsApproved { get; set; } = true;
        public string IndustryType { get; set; } = string.Empty;
    }
}
