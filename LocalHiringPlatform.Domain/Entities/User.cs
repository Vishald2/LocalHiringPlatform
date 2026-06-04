using LocalHiringPlatform.Domain.Enums;

namespace LocalHiringPlatform.Domain.Entities;

public class User:BaseEntity
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}