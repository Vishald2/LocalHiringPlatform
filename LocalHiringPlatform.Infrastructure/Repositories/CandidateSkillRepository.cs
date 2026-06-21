using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories
{

    public class CandidateSkillRepository
        : ICandidateSkillRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateSkillRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CandidateSkill>>
            GetByCandidateProfileIdAsync(Guid candidateProfileId)
        {
            return await _dbContext
                .CandidateSkills
                .Include(x => x.Skill)
                .Where(x =>
                    x.CandidateProfileId
                    == candidateProfileId)
                .ToListAsync();
        }

        public async Task AddRangeAsync(
            List<CandidateSkill> candidateSkills)
        {
            await _dbContext
                .CandidateSkills
                .AddRangeAsync(candidateSkills);
        }

        public Task RemoveRangeAsync(
            List<CandidateSkill> candidateSkills)
        {
            _dbContext
                .CandidateSkills
                .RemoveRange(candidateSkills);

            return Task.CompletedTask;
        }
    }

}