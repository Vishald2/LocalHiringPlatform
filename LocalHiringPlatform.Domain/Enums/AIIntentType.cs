using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Enums
{
    public enum AIIntentType
    {
        Unknown = 0,

        Greeting = 1,

        JobSearch = 2,

        CandidateQuestion = 3,

        EmployerQuestion = 4
    }
}
