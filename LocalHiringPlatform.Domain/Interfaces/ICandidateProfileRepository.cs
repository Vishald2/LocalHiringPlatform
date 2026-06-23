
namespace LocalHiringPlatform.Domain.Interfaces;

public interface ICandidateProfileRepository
{
    Task AddAsync(CandidateProfile profile);

    Task<CandidateProfile?> GetByUserIdAsync(Guid userId);

    Task<CandidateProfile?> GetByProfileIdAsync(Guid candidateProfileId);

    void Update(CandidateProfile profile);

    Task<List<CandidateProfile>>SearchAsync(string? name, string? city, Guid? skillId);
}