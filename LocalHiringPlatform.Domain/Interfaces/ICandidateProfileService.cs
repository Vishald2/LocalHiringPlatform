using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Http;

public interface ICandidateProfileService
{
    Task<CandidateProfileModel?> GetProfileAsync(Guid userId);

    Task UpdateProfileAsync(Guid userId, UpdateCandidateProfileModel model);

    Task UploadResumeAsync(Guid userId, IFormFile file);

    Task<List<JobModel>> GetRecommendedJobsAsync(Guid userId);
}