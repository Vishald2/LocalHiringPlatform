using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class JobSearchResultModel
    {
        public JobModel Job { get; set; } = new();

        public int MatchScore { get; set; }
    }
}
