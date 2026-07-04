namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIChatResponseModel
    {
        public string Reply { get; set; } = "";

        public AIIntentModel? Intent { get; set; }
    }
}