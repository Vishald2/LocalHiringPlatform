using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class MyApplicationModel
    {
        public Guid JobId { get; set; }

        public string JobTitle { get; set; }
            = string.Empty;

        public DateTime AppliedOn { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}
