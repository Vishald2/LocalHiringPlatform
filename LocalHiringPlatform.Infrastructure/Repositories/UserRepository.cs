using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        _dbContext.Users.Add(user);
    }

    public async Task<User?> GetByEmailAsync(
        string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByMobileAsync(
        string mobileNumber)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(
                x => x.MobileNumber == mobileNumber);
    }
    public async Task<User?> GetByEmailOrMobileAsync(
    string emailOrMobile)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(
                x =>
                    x.Email == emailOrMobile ||
                    x.MobileNumber == emailOrMobile);
    }

    public async Task<User?>GetByEmailVerificationTokenAsync(string token)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.EmailVerificationToken == token
            && x.CreatedOn.AddDays(2) > DateTime.UtcNow );
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.EntityId == userId);
    }
}