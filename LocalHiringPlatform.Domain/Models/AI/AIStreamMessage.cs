using LocalHiringPlatform.Domain.Enums;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIStreamMessage
    {
        public AIStreamMessageType Type { get; set; }

        public string? Content { get; set; }

        public object? Data { get; set; }
    }
}
