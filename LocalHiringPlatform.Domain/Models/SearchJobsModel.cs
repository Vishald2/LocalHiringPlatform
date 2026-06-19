using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class SearchJobsModel
    {
        public string? Keyword
        {
            get;
            set;
        }

        public string? City
        {
            get;
            set;
        }
    }
}
