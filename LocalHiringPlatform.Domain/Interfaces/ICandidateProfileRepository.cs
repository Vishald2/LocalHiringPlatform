using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface ICandidateProfileRepository
{
    Task AddAsync(CandidateProfile profile);
}