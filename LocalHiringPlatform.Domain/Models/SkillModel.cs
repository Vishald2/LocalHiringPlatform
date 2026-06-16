using LocalHiringPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class SkillModel
    {
        public  Guid EntityId { get; set; }
        public string SkillName { get; set; } = string.Empty;

        public SkillCategory SkillCategory { get; set; }

        public bool IsApproved { get; set; } = true;
    }
}
