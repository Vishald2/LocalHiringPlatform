using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class InternalJobDataModel
    {
        public Guid JobId { get; set; }

        public string Title { get; set; } = string.Empty;

        public Guid EmployerUserId { get; set; }
    }
}
