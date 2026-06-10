using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories
{

    public class JobApplicationRepository
        : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public JobApplicationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            JobApplication application)
        {
            await _dbContext.JobApplications
                .AddAsync(application);
        }

        public async Task<JobApplication?>
            GetByJobAndCandidateAsync(
                Guid jobId,
                Guid candidateProfileId)
        {
            return await _dbContext.JobApplications
                .FirstOrDefaultAsync(
                    x =>
                        x.JobId == jobId &&
                        x.CandidateProfileId
                            == candidateProfileId);
        }
    }
}