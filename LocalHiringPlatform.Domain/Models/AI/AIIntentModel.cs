using LocalHiringPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIIntentModel
    {
        public AIIntentType IntentType { get; set; }

        public JobSearchIntentModel? JobSearch { get; set; }
    }
}
