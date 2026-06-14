using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _dbContext;

    public JobRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Job job)
    {
        await _dbContext.Jobs.AddAsync(job);
    }

    public async Task<List<Job>> GetAllAsync()
    {
        return await _dbContext.Jobs
            .ToListAsync();
    }

    public async Task<List<Job>> GetByEmployerProfileIdAsync(Guid employerProfileId)
    {
        var abc = await _dbContext.Jobs
                        .Include(x => x.JobApplications)
                        .Where(x =>
                            x.EmployerProfileId ==
                            employerProfileId)
                        .ToListAsync();
        return abc;
    }

    public async Task<Job?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Jobs
            .FirstOrDefaultAsync(
                x => x.EntityId == id);
    }

    public Task UpdateAsync(Job job)
    {
        _dbContext.Jobs.Update(job);

        return Task.CompletedTask;
    }
}