
namespace LocalHiringPlatform.Domain.Models
{
    public class NotificationModel
    {
        public Guid EntityId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
