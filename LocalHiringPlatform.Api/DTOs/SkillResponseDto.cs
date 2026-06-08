using LocalHiringPlatform.Domain.Enums;

namespace LocalHiringPlatform.Api.DTOs
{
    public class SkillResponseDto
    {
        public string SkillName { get; set; } = string.Empty;

        public SkillCategory SkillCategory { get; set; }

        public bool IsApproved { get; set; } = true;
    }
}
