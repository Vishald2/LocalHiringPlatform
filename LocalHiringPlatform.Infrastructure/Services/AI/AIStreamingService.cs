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

        public Task CompleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(string userId, string error)
        {
            throw new NotImplementedException();
        }

        public Task ProgressAsync(string userId, string progress)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(
            string userId, 
            string connectionId,
            AIStreamMessage message)
        {
            Console.WriteLine("Sending token to connectionId: " + connectionId);
            return _hubContext
                .Clients.Client(connectionId)
                .SendAsync("ReceiveAIMessage", message);
        }

        public Task StatusAsync(string userId, string status)
        {
            throw new NotImplementedException();
        }

        public Task TokenAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
