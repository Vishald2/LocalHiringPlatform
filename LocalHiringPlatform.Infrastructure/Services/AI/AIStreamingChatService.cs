using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
using Microsoft.Extensions.Logging;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
        public class AIStreamingChatService : IAIStreamingChatService
        {
            private readonly ILLMService _llmService;
            private readonly IAIStreamingService _aiStreamingService;
            private readonly ILogger<AIStreamingChatService> _logger;

            public AIStreamingChatService(
                ILLMService llmService,
                IAIStreamingService aiStreamingService,
                ILogger<AIStreamingChatService> logger)
            {
                _llmService = llmService;
                _aiStreamingService = aiStreamingService;
                _logger = logger;
            }
        public async Task StreamAsync(
            string userId,
            AIStreamingRequest aIStreamingRequest,
            CancellationToken cancellationToken)
        {
            try
            {
                await foreach (var token in _llmService.StreamAsync(aIStreamingRequest.Message, cancellationToken))
                {
                    Console.WriteLine("Token: {0}", token);

                    await SendTokenAsync(userId, token);
                }

                await SendCompletedAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while streaming AI response.");

                await _aiStreamingService.SendAsync(
                    userId,
                    new AIStreamMessage
                    {
                        Type = AIStreamMessageType.Error,
                        Content = "Unable to generate AI response."
                    });

                throw;
            }
        }

        private Task SendTokenAsync(string userId, string token)
        {
            return _aiStreamingService.SendAsync(
                userId,
                new AIStreamMessage
                {
                    Type = AIStreamMessageType.Token,
                    Content = token
                });
        }

        private Task SendCompletedAsync(string userId)
        {
            return _aiStreamingService.SendAsync(
                userId,
                new AIStreamMessage
                {
                    Type = AIStreamMessageType.Completed
                });
        }

        private Task SendErrorAsync(string userId, string message)
        {
            return _aiStreamingService.SendAsync(
                userId,
                new AIStreamMessage
                {
                    Type = AIStreamMessageType.Error,
                    Content = message
                });
        }
    }
}
