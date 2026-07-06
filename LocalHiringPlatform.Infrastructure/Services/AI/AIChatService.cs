using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    public class AIChatService : IAIChatService
    {
        private readonly IPromptService _promptService;

        private readonly ILLMService _llmService;

        private readonly IEnumerable<IAIIntentHandler> _intentHandlers;
        private readonly ILogger<AIChatService> _logger;

        public AIChatService(
            IPromptService promptService,
            ILLMService llmService,
            IEnumerable<IAIIntentHandler> intentHandlers,
            ILogger<AIChatService> logger)
        {
            _promptService = promptService;
            _llmService = llmService;
            _intentHandlers = intentHandlers;
            _logger = logger;
        }

        public async Task<AIChatServiceResponse> SendMessageAsync(
            AIChatRequestModel request)
        {
            var promptTemplate =
                await _promptService.GetPromptAsync(
                    "UserIntentPrompt.txt");

            var prompt =
                $"{promptTemplate}\n\nUser Request:\n{request.Message}";

            var aiReply =
                await _llmService.GenerateAsync(
                    prompt);


            List<AIIntentModel>? intent = null;

            try
            {
                intent = JsonSerializer.Deserialize<List<AIIntentModel>>(
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
                intent = null;
            }

            if (intent == null)
            {
                return new AIChatServiceResponse
                {
                    Response = new List<AIIntentHandlerResponse>
                    {
                        new AIIntentHandlerResponse
                        {
                            Intent = AIIntentType.Unknown.ToString(),
                            Data = null
                        }
                    }
                };
            }

            /*GETTING THE APPROPRIATE INTENT HANDLER.
             IEnumerable<IAIIntentHandler> _intentHandlers CONTAINS ALL 
            THE IMPLEMENTATIONS OF IAIIntentHandler INTERFACE.
            This is called the Strategy Pattern
             */

            List<AIIntentHandlerResponse> responses = new List<AIIntentHandlerResponse>();

            foreach (var intentModel in intent)
            {
                try
                {
                    var handlerForIntent =
                        _intentHandlers.FirstOrDefault(
                            h => h.IntentType == intentModel.IntentType);

                    if (handlerForIntent != null)
                    {
                        var response =
                            await handlerForIntent.HandleAsync(intentModel, request);
                        responses.Add(response);
                    }
                    else
                    {
                        responses.Add(new AIIntentHandlerResponse
                        {
                            Intent = intentModel.IntentType.ToString(),
                            Data = "Sorry, I could not find a handler for this intent."
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the intent.");

                    responses.Add(new AIIntentHandlerResponse
                    {
                        Intent = intentModel.IntentType.ToString(),
                        Data = "Sorry, something went wrong while processing this request."
                    });
                }
            }

            return new AIChatServiceResponse
            {
                Response = responses
            };
        }
    }
}