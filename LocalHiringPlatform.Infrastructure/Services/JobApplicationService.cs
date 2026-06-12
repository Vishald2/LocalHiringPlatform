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
    private IEmployerProfileRepository _employerProfileRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    public JobApplicationService(
        IJobApplicationRepository
            jobApplicationRepository,
        ICandidateProfileRepository
            candidateProfileRepository,
        IEmployerProfileRepository employerProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _jobApplicationRepository =
            jobApplicationRepository;

        _candidateProfileRepository =
            candidateProfileRepository;

        _employerProfileRepository = employerProfileRepository;

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

    public async Task<List<ApplicantModel>> GetAllApplicantsByEmployerProfile(Guid userId)
    {
        var employerProfile =  await _employerProfileRepository
               .GetByUserIdAsync(userId);

        var applications = await _jobApplicationRepository.GetAllApplicantsByEmployerProfile(employerProfile.EntityId);
        return applications.Select(x => new ApplicantModel
        {
            CandidateProfileId = x.CandidateProfileId,
            CandidateName = $"{x.CandidateProfile.FullName}",
            Email = x.CandidateProfile.User.Email,
            MobileNumber = x.CandidateProfile.User.MobileNumber,
            AppliedOn = x.AppliedOn,
            Status = x.Status,
            JobId = x.JobId,
            JobTitle = x.Job.Title
        }).ToList();
    }

    public async Task<List<ApplicantModel>> GetApplicantsAsync(Guid jobId, Guid userId)
    {
        var applications = await _jobApplicationRepository.GetByJobIdAsync(jobId);

        return applications
            .Select(x =>
                new ApplicantModel
                {
                    CandidateProfileId =
                        x.CandidateProfileId,

                    CandidateName =
                        $"{x.CandidateProfile.FullName}",

                    Email =
                        x.CandidateProfile
                            .User
                            .Email,

                    MobileNumber =
                        x.CandidateProfile
                            .User
                            .MobileNumber,

                    AppliedOn =
                        x.AppliedOn,

                    Status =
                        x.Status
                })
            .ToList();
    }

    public async Task<List<MyApplicationModel>>  GetMyApplicationsAsync(
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

        var applications =
            await _jobApplicationRepository
                .GetByCandidateProfileIdAsync(
                    candidateProfile.EntityId);

        return applications
            .Select(x =>
                new MyApplicationModel
                {
                    JobId = x.JobId,

                    JobTitle =
                        x.Job.Title,

                    AppliedOn =
                        x.AppliedOn,

                    Status =
                        x.Status
                })
            .ToList();
    }

    public async Task UpdateStatusAsync(UpdateApplicationStatusModel model, Guid userId)
    {
        var employerProfile =
            await _employerProfileRepository
                .GetByUserIdAsync(userId);

        if (employerProfile == null)
        {
            throw new Exception(
                "Employer profile not found.");
        }

        var application =
            await _jobApplicationRepository.GetByIdAsync(model.JobApplicationId);

        if (application == null)
        {
            throw new Exception(
                "Application not found.");
        }

        if (application.Job.EmployerProfileId != employerProfile.EntityId)
        {
            throw new UnauthorizedAccessException(
                "You cannot update this application.");
        }

        application.Status = model.Status;

        await _unitOfWork.SaveChangesAsync();
    }
}