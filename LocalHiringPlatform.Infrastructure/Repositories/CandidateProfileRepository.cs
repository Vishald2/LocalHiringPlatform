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
        return await _dbContext.CandidateProfiles.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public void Update(
        CandidateProfile profile)
    {
        _dbContext.CandidateProfiles.Update(
            profile);
    }

    public async Task<List<CandidateProfile>>
    SearchAsync(
        string? name,
        string? city,
        Guid? skillId)
    {
        var query =
            _dbContext.CandidateProfiles
                .Include(x => x.User)
                .Include(x => x.CandidateSkills)
                    .ThenInclude(x => x.Skill)
                .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query =
                query.Where(x =>
                    x.FullName.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query =
                query.Where(x =>
                    x.City.Contains(city));
        }

        if (skillId.HasValue)
        {
            query =
                query.Where(x =>
                    x.CandidateSkills
                        .Any(cs =>
                            cs.SkillId ==
                            skillId.Value));
        }

        return await query.ToListAsync();
    }
}