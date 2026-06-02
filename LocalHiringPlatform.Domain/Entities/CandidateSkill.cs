namespace LocalHiringPlatform.Domain.Entities;
public class CandidateSkill : BaseEntity
{
    public Guid Id { get; set; }

    public Guid CandidateProfileId { get; set; }

    public Guid SkillId { get; set; }

    public int ExperienceInMonths { get; set; }
}