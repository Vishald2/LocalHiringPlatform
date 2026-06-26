using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories
{
    using LocalHiringPlatform.Domain.Entities;
    using LocalHiringPlatform.Domain.Interfaces;
    using LocalHiringPlatform.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class SavedJobRepository : Repository<SavedJob>, ISavedJobRepository
    {
        public SavedJobRepository(ApplicationDbContext dbContext)
                                : base(dbContext)
        {

        }

        public async Task<SavedJob?> GetAsync(Guid userId, Guid jobId)
        {
            return await _dbContext
                .SavedJobs
                .FirstOrDefaultAsync(
                    x =>
                        x.UserId == userId
                        &&
                        x.JobId == jobId);
        }

        public async Task<List<SavedJob>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext
                .SavedJobs
                .Include(x => x.Job)
                .Where(
                    x => x.UserId == userId)
                .OrderByDescending(
                    x => x.CreatedOn)
                .ToListAsync();
        }
    }
}
