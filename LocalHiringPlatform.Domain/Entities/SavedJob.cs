using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities
{
    public class SavedJob : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid JobId { get; set; }

        public User User { get; set; } = null!;

        public Job Job { get; set; } = null!;
    }
}
