using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class JobSearchAIModel
    {
        public List<string> City { get; set; } = new();

        public List<string> State { get; set; } = new();

        public List<string> RequiredSkills { get; set; } = new();

        public string? MinExperienceRequired { get; set; }

        public string? MaxExperienceRequired { get; set; }

        public string? MinSalary { get; set; }

        public string? MaxSalary { get; set; }
    }
}
