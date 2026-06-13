using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class UpdateJobModel
    {
        public Guid EntityId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public int ExperienceRequired { get; set; }

        public string? RequiredSkills { get; set; }

        public bool IsActive { get; set; }
    }
}
