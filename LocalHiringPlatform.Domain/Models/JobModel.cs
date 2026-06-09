namespace LocalHiringPlatform.Domain.Models;

public class JobModel
{
    public Guid EntityId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    public int ExperienceRequired { get; set; }

    public string? RequiredSkills { get; set; }

    public bool IsActive { get; set; }
}