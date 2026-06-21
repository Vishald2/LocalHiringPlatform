namespace LocalHiringPlatform.Domain.Entities;
public class CandidateSkill : BaseEntity
{
    public Guid CandidateSkillId
    {
        get => EntityId;
        set => EntityId = value;
    }

    public Guid CandidateProfileId { get; set; }

    public Guid SkillId { get; set; }

    public int ExperienceInMonths { get; set; }


    /* Navigation Properties */

    public CandidateProfile CandidateProfile { get; set; } = null!;

    public Skill Skill { get; set; } = null!;
}