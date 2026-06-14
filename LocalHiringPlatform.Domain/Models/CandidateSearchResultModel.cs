using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class CandidateSearchResultModel
    {
        public Guid CandidateProfileId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public decimal TotalExperienceYears { get; set; }

        public string? ResumeFileName { get; set; }

        public string? ResumeFilePath { get; set; }

        public bool IsOpenToWork { get; set; }
    }
}
