namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIStreamingRequest
    {
        public string Message { get; set; } = string.Empty;

        public string connectionId { get; set; }= string.Empty;
    }
}
