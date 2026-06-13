using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJobService
{
    Task AddJobAsync(
    CreateJobModel model,
    Guid userId);

    Task<List<JobModel>> GetAllJobsAsync();

    Task<JobModel?> GetJobAsync(Guid id);

    Task<List<JobModel>> GetEmployerJobsAsync(Guid userId);

    Task UpdateJobAsync(Guid userId, UpdateJobModel model);
}