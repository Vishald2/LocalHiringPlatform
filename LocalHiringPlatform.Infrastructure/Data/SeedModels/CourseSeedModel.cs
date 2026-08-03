using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Data.SeedModels
{
    public class CourseSeedModel
    {
        public string EducationCode { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
