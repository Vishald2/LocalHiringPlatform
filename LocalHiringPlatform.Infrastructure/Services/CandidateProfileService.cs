using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace LocalHiringPlatform.Infrastructure.Services;

public class CandidateProfileService
    : ICandidateProfileService
{
    private readonly ICandidateProfileRepository
        _candidateProfileRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    private readonly IUserRepository _userRepository;
    private readonly IJobRepository _jobRepository;

    public CandidateProfileService(
        ICandidateProfileRepository candidateProfileRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJobRepository jobRepository
        )
    {
        _candidateProfileRepository = candidateProfileRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jobRepository = jobRepository;
    }

    public async Task<CandidateProfileModel?> GetProfileAsync(Guid userId)
    {
        var profile =
            await _candidateProfileRepository
                .GetByUserIdAsync(userId);

        if (profile == null)
        {
            return null;
        }

        return new CandidateProfileModel
        {
            FullName = profile.FullName,
            DateOfBirth = profile.DateOfBirth,
            Gender = profile.Gender,
            City = profile.City,
            State = profile.State,
            CurrentSalary = profile.CurrentSalary,
            ExpectedSalary = profile.ExpectedSalary,
            TotalExperienceYears = profile.TotalExperienceYears,
            ProfileSummary = profile.ProfileSummary,
            IsOpenToWork = profile.IsOpenToWork,
            ProfileCompletionPercentage =  profile.ProfileCompletionPercentage,
            ResumeFileName = profile.ResumeFileName,
            ResumeFilePath = profile.ResumeFilePath,
            EmailVerified =  profile.User.EmailVerified,
            MobileNumber = profile.User.MobileNumber,
            MobileVerified = profile.User.MobileVerified
        };
    }

    public async Task UpdateProfileAsync(Guid userId, UpdateCandidateProfileModel model)
    {
        var profile = await _candidateProfileRepository
                .GetByUserIdAsync(userId);

        if (profile == null)
        {
            throw new BusinessException(
                "Candidate profile not found");
        }

        profile.FullName =
            model.FullName;

        profile.DateOfBirth =
            model.DateOfBirth;

        profile.Gender =
            model.Gender;

        profile.City =
            model.City;

        profile.State =
            model.State;

        profile.CurrentSalary =
            model.CurrentSalary;

        profile.ExpectedSalary =
            model.ExpectedSalary;

        profile.TotalExperienceYears =
            (decimal)model.TotalExperienceYears;

        profile.ProfileSummary =
            model.ProfileSummary;

        profile.IsOpenToWork =
            model.IsOpenToWork;

        profile.ProfileCompletionPercentage =
            CalculateProfileCompletion(profile);

        _candidateProfileRepository
            .Update(profile);

        await _unitOfWork
            .SaveChangesAsync();
    }

    private static int CalculateProfileCompletion(
        CandidateProfile profile)
    {
        int score = 0;

        if (!string.IsNullOrWhiteSpace(profile.FullName))
            score += 10;

        if (profile.DateOfBirth != null)
            score += 10;

        if (profile.Gender != null)
            score += 10;

        if (!string.IsNullOrWhiteSpace(profile.City))
            score += 10;

        if (!string.IsNullOrWhiteSpace(profile.State))
            score += 10;

        if (profile.CurrentSalary != null)
            score += 10;

        if (profile.ExpectedSalary != null)
            score += 10;

        if (profile.TotalExperienceYears != null)
            score += 10;

        if (!string.IsNullOrWhiteSpace(
                profile.ProfileSummary))
            score += 20;

        return score;
    }

    public async Task UploadResumeAsync(Guid userId, IFormFile file)
    {
        var profile =
            await _candidateProfileRepository
                .GetByUserIdAsync(userId);

        if (profile == null)
        {
            throw new Exception("Candidate profile not found.");
        }

        if (file == null || file.Length == 0)
        {
            throw new Exception("No file uploaded.");
        }

        var uploadsFolder =
            Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "resumes");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(
                uploadsFolder);
        }

        var fileName =
            $"{Guid.NewGuid()}_{file.FileName}";

        var filePath =
            Path.Combine(
                uploadsFolder,
                fileName);

        using (var stream =
            new FileStream(
                filePath,
                FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        profile.ResumeFileName =
            file.FileName;

        profile.ResumeFilePath =
            $"/resumes/{fileName}";

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<JobModel>> GetRecommendedJobsAsync(
        Guid userId)
    {
        var candidateProfile =
            await _candidateProfileRepository
                .GetByUserIdAsync(userId);

        if (candidateProfile == null)
        {
            throw new BusinessException(
                "Candidate profile not found.");
        }

        var candidateSkills =
            candidateProfile
                .CandidateSkills
                .Select(x => x.Skill.SkillName)
                .ToList();

        var jobs =
            await _jobRepository
                .GetAllAsync();

        var recommendedJobs =
            jobs
            .Where(x => x.IsActive)
            .Select(job =>
            {
                var requiredSkills =
                    (job.RequiredSkills ?? "")
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

                int matchPercentage = 0;

                if (requiredSkills.Any())
                {
                    int matchedSkills =
                        candidateSkills.Count(
                            cs =>
                                requiredSkills.Any(
                                    rs =>
                                        rs.Equals(
                                            cs,
                                            StringComparison.OrdinalIgnoreCase)));

                    matchPercentage =
                        (matchedSkills * 100)
                        / requiredSkills.Count;
                }

                return new
                {
                    Job = job,
                    MatchPercentage = matchPercentage
                };
            })
            .Where(x => x.MatchPercentage > 0)
            .OrderByDescending(x => x.MatchPercentage)
            .Select(x => new JobModel
            {
                EntityId = x.Job.EntityId,
                Title = x.Job.Title,
                Description = x.Job.Description,
                City = x.Job.City,
                State = x.Job.State,
                MinSalary = x.Job.MinSalary,
                MaxSalary = x.Job.MaxSalary,
                ExperienceRequired = x.Job.ExperienceRequired,
                RequiredSkills = x.Job.RequiredSkills,
                IsActive = x.Job.IsActive
            })
            .ToList();

        return recommendedJobs;
    }
}