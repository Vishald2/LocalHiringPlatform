using System;
using System.Collections.Generic;
using System.Text;

namespace LocalHiringPlatform.Domain.Entities
{
    public class Skill:BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsApproved { get; set; } = true;
    }
}
