using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class AiMatchResultModel
    {
        public int Score { get; set; }

        public string Recommendation { get; set; }
            = string.Empty;

        public List<string> Strengths { get; set; }
            = new();

        public List<string> Gaps { get; set; }
            = new();
    }
}
