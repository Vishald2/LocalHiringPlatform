using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Models.AI;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface IAIIntentHandler
    {
        AIIntentType IntentType { get; }

        Task<AIIntentHandlerResponse> HandleAsync(
            AIIntentModel intentModel, AIChatRequestModel request);
    }
}
