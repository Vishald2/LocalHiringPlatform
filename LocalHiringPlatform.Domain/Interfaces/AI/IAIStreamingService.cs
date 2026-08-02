using LocalHiringPlatform.Domain.Models.AI;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface IAIStreamingService
    {
        Task SendAsync(
            string connectionId,
            AIStreamMessage message);

    }
}
