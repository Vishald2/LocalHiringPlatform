using LocalHiringPlatform.Domain.Enums;

namespace LocalHiringPlatform.Api.DTOs;

public class CandidateProfileResponseDto
{
    public string FullName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public decimal? CurrentSalary { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public decimal? TotalExperienceYears { get; set; }

    public string? ProfileSummary { get; set; }

    public bool IsOpenToWork { get; set; }

    public int ProfileCompletionPercentage { get; set; }
}