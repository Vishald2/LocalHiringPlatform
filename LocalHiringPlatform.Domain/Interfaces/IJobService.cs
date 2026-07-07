using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Domain.Models.AI;

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
    Task<List<JobModel>> SearchJobsAsync(SearchJobsModel model);

    Task<List<JobSearchResultModel>> SearchAsync(JobSearchModel jobSearchModel);
}