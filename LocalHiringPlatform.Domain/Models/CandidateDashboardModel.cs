using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class CandidateDashboardModel
    {
        public int TotalApplications { get; set; }

        public int Shortlisted { get; set; }

        public int InterviewScheduled { get; set; }

        public int Rejected { get; set; }

        public int Hired { get; set; }
    }
}
