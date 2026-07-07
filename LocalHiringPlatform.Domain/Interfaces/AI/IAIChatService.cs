using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    using global::LocalHiringPlatform.Domain.Models.AI;
    using LocalHiringPlatform.Domain.Interfaces.AI;

    namespace LocalHiringPlatform.Domain.Interfaces.AI
    {
        public interface IAIChatService
        {
            Task<AIChatServiceResponse> SendMessageAsync(
                AIChatRequestModel request);
        }
    }
}
