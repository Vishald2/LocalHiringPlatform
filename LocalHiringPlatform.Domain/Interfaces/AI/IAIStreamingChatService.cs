using LocalHiringPlatform.Domain.Models.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface IAIStreamingChatService
    {
        Task StreamAsync(string userId, AIStreamingRequest message, CancellationToken cancellationToken);
    }
}
