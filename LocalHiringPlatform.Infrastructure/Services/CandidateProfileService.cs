using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Infrastructure.Services;

public class CandidateProfileService
    : ICandidateProfileService
{
    private readonly ICandidateProfileRepository
        _candidateProfileRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    public CandidateProfileService(
        ICandidateProfileRepository candidateProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _candidateProfileRepository =
            candidateProfileRepository;

        _unitOfWork =
            unitOfWork;
    }

    public async Task<CandidateProfileModel?>
        GetProfileAsync(Guid userId)
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
            TotalExperienceYears =
                profile.TotalExperienceYears,
            ProfileSummary =
                profile.ProfileSummary,
            IsOpenToWork =
                profile.IsOpenToWork,
            ProfileCompletionPercentage =
                profile.ProfileCompletionPercentage
        };
    }

    public async Task UpdateProfileAsync(
        Guid userId,
        UpdateCandidateProfileModel model)
    {
        var profile =
            await _candidateProfileRepository
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
}