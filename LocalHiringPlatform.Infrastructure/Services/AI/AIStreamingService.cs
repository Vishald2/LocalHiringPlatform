using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
using LocalHiringPlatform.Domain.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    public class AIStreamingService : IAIStreamingService
    {
        private readonly IHubContext<AIHub> _hubContext;

        public AIStreamingService(
            IHubContext<AIHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendAsync(
            string connectionId,
            AIStreamMessage message)
        {
            Console.WriteLine("Sending token to connectionId: " + connectionId);
            return _hubContext
                .Clients.Client(connectionId)
                .SendAsync("ReceiveAIMessage", message);
        }
    }
}
