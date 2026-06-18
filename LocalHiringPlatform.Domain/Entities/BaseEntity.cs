using System.ComponentModel.DataAnnotations;

namespace LocalHiringPlatform.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid EntityId { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
