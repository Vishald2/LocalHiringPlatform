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

    public async Task<List<Job>>SearchAsync(string? keyword, string? city)
    {
        var jobs = await _dbContext.Jobs.ToListAsync();

        IQueryable<Job> query =
            _dbContext.Jobs
                .Where(x => x.IsActive);

        keyword = keyword?.Trim().ToLower();
        city = city?.Trim().ToLower();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(
                x =>
                    x.Title.ToLower().Contains(keyword)
                    ||
                    (x.Description ?? "")
                        .ToLower()
                        .Contains(keyword)
                    ||
                    (x.RequiredSkills ?? "")
                        .ToLower()
                        .Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(
                x =>
                    (x.City ?? "")
                        .ToLower()
                        .Contains(city));
        }

        //  return result;

        return await query
            .ToListAsync();
    }
}