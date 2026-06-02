using System;
using System.Collections.Generic;
using System.Text;

namespace LocalHiringPlatform.Domain.Entities
{
    public class CandidateSavedJob : BaseEntity
    {
        public Guid CandidateProfileId { get; set; }
        public Guid JobId { get; set; }
    }
}
