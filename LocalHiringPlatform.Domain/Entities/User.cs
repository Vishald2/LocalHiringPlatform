using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Enums;
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public bool EmailVerified { get; set; }
    public string? EmailVerificationToken { get; set; }
    public DateTime? EmailVerificationTokenExpiry { get; set; }
    
    public bool MobileVerified { get; set; }
    public string? MobileVerificationCode { get; set; }
    public DateTime? MobileVerificationCodeExpiry { get; set; }
    public DateTime? EmailVerifiedOn { get; set; }
    public DateTime? MobileVerifiedOn { get; set; }
    public CandidateProfile? CandidateProfile { get; set; }

    public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();
}