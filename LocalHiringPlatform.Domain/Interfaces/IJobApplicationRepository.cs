using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task AddAsync(
            JobApplication application);

        Task<JobApplication?>
            GetByJobAndCandidateAsync(
                Guid jobId,
                Guid candidateProfileId);
    }
}