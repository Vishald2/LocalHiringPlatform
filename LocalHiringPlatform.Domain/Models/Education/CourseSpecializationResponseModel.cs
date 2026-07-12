using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.Education
{
    public class CourseSpecializationResponseModel
    {
        public int CourseSpecializationId { get; set; }

        public int SpecializationId { get; set; }

        public string SpecializationName { get; set; } = string.Empty;
    }
}
