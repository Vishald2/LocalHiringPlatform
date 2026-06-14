namespace LocalHiringPlatform.Domain.Entities;
public class CandidateSkill : BaseEntity
{
    public Guid Id { get; set; }

    public Guid CandidateProfileId { get; set; }

    public Guid SkillId { get; set; }

    public int ExperienceInMonths { get; set; }


    /* Navigation Properties */

    public CandidateProfile CandidateProfile { get; set; } = null!;

    public Skill Skill { get; set; } = null!;
}