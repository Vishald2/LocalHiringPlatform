using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories
{
    public class AiAnalysisRepository : IAiAnalysisRepository
    {
        private readonly ApplicationDbContext
            _dbContext;

        public AiAnalysisRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext =
                dbContext;
        }

        public async Task<AiAnalysis?>
            GetByJobApplicationIdAsync(
                Guid jobApplicationId)
        {
            return await _dbContext
                .AiAnalyses
                .FirstOrDefaultAsync(
                    x =>
                        x.JobApplicationId
                        == jobApplicationId);
        }

        public async Task AddAsync(
            AiAnalysis aiAnalysis)
        {
            await _dbContext
                .AiAnalyses
                .AddAsync(aiAnalysis);
        }

        public void Update(
            AiAnalysis aiAnalysis)
        {
            _dbContext
                .AiAnalyses
                .Update(aiAnalysis);
        }
    }
}
