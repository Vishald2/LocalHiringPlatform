using LocalHiringPlatform.Domain.Entities;

public class EmployerProfile : BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? Industry { get; set; }

    public string? Website { get; set; }

    public string? CompanyDescription { get; set; }

    public User? User { get; set; } = null;

    public ICollection<Job> Jobs { get; set; }  = new List<Job>();
}