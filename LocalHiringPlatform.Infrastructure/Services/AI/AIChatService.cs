using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    public class AIChatService : IAIChatService
    {
        private readonly IPromptService _promptService;

        private readonly ILLMService _llmService;

        private readonly IEnumerable<IAIIntentHandler> _intentHandlers;

        public AIChatService(
            IPromptService promptService,
            ILLMService llmService,
            IEnumerable<IAIIntentHandler> intentHandlers)
        {
            _promptService = promptService;
            _llmService = llmService;
            _intentHandlers = intentHandlers;
        }

        public async Task<AIChatResponseModel> SendMessageAsync(
            AIChatRequestModel request)
        {
            var promptTemplate =
                await _promptService.GetPromptAsync(
                    "JobSearchIntentPrompt.txt");

            var prompt =
                $"{promptTemplate}\n\nUser Request:\n{request.Message}";

            var aiReply =
                await _llmService.GenerateAsync(
                    prompt);


            AIIntentModel? intent = null;

            try
            {
                intent =
                    JsonSerializer.Deserialize<AIIntentModel>(
                        aiReply,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            Converters =
                            {
                new JsonStringEnumConverter()
                            }
                        });
            }
            catch
            {
                intent = new AIIntentModel
                {
                    IntentType = AIIntentType.Unknown
                };
            }

            if (intent == null)
            {
                return new AIChatResponseModel
                {
                    Reply = "Sorry, I could not understand your request."
                };
            }

            /*GETTING THE APPROPRIATE INTENT HANDLER.
             IEnumerable<IAIIntentHandler> _intentHandlers CONTAINS ALL 
            THE IMPLEMENTATIONS OF IAIIntentHandler INTERFACE.
            This is called the Strategy Pattern
             */
            var handler =
                _intentHandlers.FirstOrDefault(
                    h => h.IntentType == intent.IntentType);

            if (handler == null)
            {
                return new AIChatResponseModel
                {
                    Reply =
                        $"Intent '{intent.IntentType}' is not implemented yet."
                };
            }

            return await handler.HandleAsync(intent);
        }
    }
}