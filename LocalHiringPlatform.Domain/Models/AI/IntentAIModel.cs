using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class IntentAIModel<T>
    {
        string IntentType { get; set; } = string.Empty;
        public T AdditionInfo { get; set; } = default!;
    }
}
