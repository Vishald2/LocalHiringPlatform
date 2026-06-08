using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories;

public class CandidateProfileRepository
    : ICandidateProfileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CandidateProfileRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        CandidateProfile profile)
    {
        _dbContext.CandidateProfiles.Add(profile);
    }

    public async Task<CandidateProfile?> GetByUserIdAsync(
        Guid userId)
    {
        return await _dbContext.CandidateProfiles
            .FirstOrDefaultAsync(
                x => x.UserId == userId);
    }

    public void Update(
        CandidateProfile profile)
    {
        _dbContext.CandidateProfiles.Update(
            profile);
    }
}