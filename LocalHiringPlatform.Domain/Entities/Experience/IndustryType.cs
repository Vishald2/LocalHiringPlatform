using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.Experience
{
    public class IndustryType : BaseEntity
    {
        public int IndustryTypeId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public string Description { get; set; } = string.Empty;
    }
}
