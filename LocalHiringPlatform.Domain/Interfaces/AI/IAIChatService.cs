using LocalHiringPlatform.Domain.Models.AI;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
        public interface IAIChatService
        {
            Task<AIChatServiceResponse> SendMessageAsync(
                AIChatRequestModel request);
        }
}
