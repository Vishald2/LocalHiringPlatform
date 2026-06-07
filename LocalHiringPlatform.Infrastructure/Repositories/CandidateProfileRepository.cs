using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;

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
}