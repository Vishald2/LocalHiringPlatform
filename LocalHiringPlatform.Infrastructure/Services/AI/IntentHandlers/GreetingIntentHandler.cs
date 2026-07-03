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

            public Task<AIChatResponseModel> HandleAsync(
                AIIntentModel intent)
            {
                return Task.FromResult(
                    new AIChatResponseModel
                    {
                        Reply =
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
