namespace LocalHiringPlatform.Domain.Entities
{
    public class BaseEntity
    {
        public Guid EntityId { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}