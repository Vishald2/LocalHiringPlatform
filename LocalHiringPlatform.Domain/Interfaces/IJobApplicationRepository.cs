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

        Task<List<JobApplication>> GetByCandidateProfileIdAsync(Guid candidateProfileId);
        Task<List<JobApplication>>GetByJobIdAsync(Guid jobId);

        Task<List<JobApplication>> GetAllApplicantsByEmployerProfile(Guid employerProfileId);

        Task<JobApplication?> GetByIdAsync(Guid id);
    }
}