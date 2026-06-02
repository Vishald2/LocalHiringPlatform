using System;
using System.Collections.Generic;
using System.Text;

namespace LocalHiringPlatform.Domain.Entities
{
    public class CandidateResume : BaseEntity
    {
        public Guid CandidateProfileId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }
    }
}
