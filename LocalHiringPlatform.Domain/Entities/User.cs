using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Enums;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;
    public CandidateProfile? CandidateProfile { get; set; }
}