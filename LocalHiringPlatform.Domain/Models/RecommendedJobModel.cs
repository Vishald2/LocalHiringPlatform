using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class RecommendedJobModel
    {
        public Guid JobId { get; set; }

        public string Title { get; set; }
            = string.Empty;

        public string City { get; set; }
            = string.Empty;

        public string State { get; set; }
            = string.Empty;

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public int MatchPercentage { get; set; }
    }
}
