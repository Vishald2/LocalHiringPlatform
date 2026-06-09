using LocalHiringPlatform.Domain.Entities;

public class Job : BaseEntity
{
    public Guid EmployerProfileId { get; set; }

    public EmployerProfile EmployerProfile { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    public int ExperienceRequired { get; set; }

    public string? RequiredSkills { get; set; }

    public bool IsActive { get; set; } = true;
}