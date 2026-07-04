using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers
{
    using global::LocalHiringPlatform.Domain.Enums;
    using global::LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
    using global::LocalHiringPlatform.Domain.Models.AI;

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
                        Response =
                        """
                        Hello! I am LocalHire AI.

                        I can help you:

                        • Find jobs
                        • Recommend skills
                        • Improve your resume
                        • Answer career questions
                        """
                    });
            }
        }
    }
}
