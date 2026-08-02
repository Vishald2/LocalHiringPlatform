using LocalHiringPlatform.Domain.Models.AI;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface IAIStreamingService
    {
        Task SendAsync(
            string userId, string connectionId,
            AIStreamMessage message);

        Task TokenAsync(string userId, string token);

        Task StatusAsync(string userId, string status);

        Task ProgressAsync(string userId, string progress);

        Task CompleteAsync(string userId);

        Task ErrorAsync(string userId, string error);

    }
}
