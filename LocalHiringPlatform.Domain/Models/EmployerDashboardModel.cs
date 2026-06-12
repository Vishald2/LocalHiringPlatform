using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    // LocalHiringPlatform.Domain/Models/EmployerDashboardModel.cs

    public class EmployerDashboardModel
    {
        public int TotalJobs { get; set; }

        public int ActiveJobs { get; set; }

        public int TotalApplicants { get; set; }

        public int NewApplications { get; set; }
    }
}
