namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIChatResponseModel
    {
        public List<AIIntentHandlerResponse> AIReply { get; set; } = new List<AIIntentHandlerResponse>();
    }
}