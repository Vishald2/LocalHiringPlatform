using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.Education
{
    public class EducationModel
    {
        public int EducationId { get; set; }

        public string EducationName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}
