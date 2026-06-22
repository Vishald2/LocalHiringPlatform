using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class CandidateSkillModel
    {
        public Guid SkillId { get; set; }

        public string SkillName { get; set; } = string.Empty;

        public string IndustryType { get; set; } = string.Empty;

        public string SkillCategory { get; set; } = string.Empty;

        public int ExperienceInMonths { get; set; }
    }
}
