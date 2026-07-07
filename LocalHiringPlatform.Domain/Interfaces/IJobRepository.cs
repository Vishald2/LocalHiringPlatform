using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Domain.Models.AI;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJobRepository
{
    Task AddAsync(Job job);

    Task<List<Job>> GetAllAsync();

    Task<Job?> GetByIdAsync(Guid id);

    Task<List<Job>> GetByEmployerProfileIdAsync(Guid employerProfileId);

    Task UpdateAsync(Job job);
    Task<List<Job>>SearchAsync(string? keyword, string? city);

    Task<List<JobSearchResultModel>> SearchAsync(JobSearchModel jobSearchModel);
}