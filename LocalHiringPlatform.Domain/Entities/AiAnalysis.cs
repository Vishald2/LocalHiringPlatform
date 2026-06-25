using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities
{
    public class AiAnalysis : BaseEntity
    {
        public Guid AiAnalysisId
        {
            get => EntityId;
            set => EntityId = value;
        }

        public Guid JobApplicationId { get; set; }

        public int Score { get; set; }

        public string Recommendation { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string Gaps { get; set; } = string.Empty;

        public DateTime AnalyzedOn { get; set; }

        public int AnalysisCount { get; set; } = 0;

        /* Navigation */

        public JobApplication JobApplication { get; set; } = null!;
    }
}
