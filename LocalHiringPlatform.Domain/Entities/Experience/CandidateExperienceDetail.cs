using System.ComponentModel.DataAnnotations.Schema;

namespace LocalHiringPlatform.Domain.Entities.Experience
{
    public class CandidateExperienceDetail : BaseEntity
    {
        [ForeignKey("CandidateExperience")]
        public Guid CandidateExperienceId { get; set; }

        public CandidateExperience CandidateExperience { get; set; } = null!;

        public string Work { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Achievements { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }
}
