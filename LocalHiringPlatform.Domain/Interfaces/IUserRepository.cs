using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByMobileAsync(string mobileNumber);
    Task<User?> GetByEmailOrMobileAsync(string emailOrMobile);
    Task<User?> GetByEmailVerificationTokenAsync(string token);
    Task<User?> GetByIdAsync(Guid userId);
}