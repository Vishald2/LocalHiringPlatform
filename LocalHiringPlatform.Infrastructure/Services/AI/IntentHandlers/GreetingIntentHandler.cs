using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;

namespace LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers
{
        public class GreetingIntentHandler : IAIIntentHandler
        {
            public AIIntentType IntentType
                => AIIntentType.Greeting;

            public Task<AIIntentHandlerResponse> HandleAsync(
                AIIntentModel intent, AIChatRequestModel request)
            {
                return Task.FromResult(
                    new AIIntentHandlerResponse
                    {
                        Intent = AIIntentType.Greeting.ToString(),
                        Data =
                        """
                        Hello! I am LocalHire AI.

                        I can help you

                        find jobs. You just have to give me place and the type of job you are looking for.
                        """
                    });
            }
        }
    }
