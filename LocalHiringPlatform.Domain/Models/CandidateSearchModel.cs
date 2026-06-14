using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class CandidateSearchModel
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        public Guid? SkillId { get; set; }
    }
}
