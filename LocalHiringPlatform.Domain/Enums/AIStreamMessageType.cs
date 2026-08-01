using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Enums
{
    public enum AIStreamMessageType
    {
        Token,
        Status,
        Progress,
        Completed,
        Error
    }
}
