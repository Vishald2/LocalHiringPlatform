using LocalHiringPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalHiringPlatform.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public Guid Id { get; set; }

        public string SkillName { get; set; } = string.Empty;

        public SkillCategory SkillCategory { get; set; }

        public bool IsApproved { get; set; } = true;
    }
}
