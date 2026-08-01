using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    using LocalHiringPlatform.Domain.Models;
    using LocalHiringPlatform.Domain.Models.AI;
    using Microsoft.AspNetCore.SignalR;

    public class AIStreamingService : IAIStreamingService
    {
        private readonly IHubContext<AIHub> _hubContext;

        public AIStreamingService(
            IHubContext<AIHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(
            string userId,
            AIStreamMessage message)
        {
            string employerUserId = "";

            await _hubContext
            .Clients
            .User(employerUserId)
            .SendAsync("ReceiveNotification", message);
        }
    }
}
