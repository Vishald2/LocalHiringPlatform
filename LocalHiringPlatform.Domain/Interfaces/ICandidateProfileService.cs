using LocalHiringPlatform.Domain.Models;

public interface ICandidateProfileService
{
    Task<CandidateProfileModel?>
        GetProfileAsync(Guid userId);

    Task UpdateProfileAsync(
        Guid userId,
        UpdateCandidateProfileModel model);
}