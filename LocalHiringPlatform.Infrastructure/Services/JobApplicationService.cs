using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services;

public class JobApplicationService
    : IJobApplicationService
{
    private readonly
        IJobApplicationRepository
        _jobApplicationRepository;

    private readonly
        ICandidateProfileRepository
        _candidateProfileRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    public JobApplicationService(
        IJobApplicationRepository
            jobApplicationRepository,
        ICandidateProfileRepository
            candidateProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _jobApplicationRepository =
            jobApplicationRepository;

        _candidateProfileRepository =
            candidateProfileRepository;

        _unitOfWork = unitOfWork;
    }

    public async Task ApplyToJobAsync(
        ApplyToJobModel model,
        Guid userId)
    {
        var candidateProfile =
            await _candidateProfileRepository
                .GetByUserIdAsync(userId);

        if (candidateProfile == null)
        {
            throw new BusinessException(
                "Candidate profile not found");
        }

        var existingApplication =
            await _jobApplicationRepository
                .GetByJobAndCandidateAsync(
                    model.JobId,
                    candidateProfile.EntityId);

        if (existingApplication != null)
        {
            throw new BusinessException(
                "You have already applied for this job");
        }

        var application =
            new JobApplication
            {
                JobId = model.JobId,

                CandidateProfileId =
                    candidateProfile.EntityId,

                AppliedOn =
                    DateTime.UtcNow,

                Status = "Applied"
            };

        await _jobApplicationRepository
            .AddAsync(application);

        await _unitOfWork
            .SaveChangesAsync();
    }
}