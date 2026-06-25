using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.Api.Controllers;

[ApiController]
[Route("api/candidate/profile")]
[Authorize]
public class CandidateProfileController : ControllerBase
{
    private readonly ICandidateProfileService
        _candidateProfileService;

    public CandidateProfileController(
        ICandidateProfileService candidateProfileService)
    {
        _candidateProfileService =
            candidateProfileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        Guid userId =
            Guid.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

        var profile =
            await _candidateProfileService
                .GetProfileAsync(userId);

        if (profile == null)
        {
            return NotFound();
        }

        var response =
            new CandidateProfileResponseDto
            {
                FullName =
                    profile.FullName,

                DateOfBirth =
                    profile.DateOfBirth,

                Gender =
                    profile.Gender,

                City =
                    profile.City,

                State =
                    profile.State,

                CurrentSalary =
                    profile.CurrentSalary,

                ExpectedSalary =
                    profile.ExpectedSalary,

                TotalExperienceYears =
                    profile.TotalExperienceYears,

                ProfileSummary =
                    profile.ProfileSummary,

                IsOpenToWork =
                    profile.IsOpenToWork,

                ProfileCompletionPercentage =
                    profile.ProfileCompletionPercentage,

                    ResumeFileName = profile.ResumeFileName,

                ResumeFilePath = profile.ResumeFilePath,

                EmailVerified=profile.EmailVerified
                
            };

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        UpdateCandidateProfileRequestDto request)
    {
        Guid userId =
            Guid.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

        var model =
            new UpdateCandidateProfileModel
            {
                FullName =
                    request.FullName,

                DateOfBirth =
                    request.DateOfBirth,

                Gender =
                    request.Gender,

                City =
                    request.City,

                State =
                    request.State,

                CurrentSalary =
                    request.CurrentSalary,

                ExpectedSalary =
                    request.ExpectedSalary,

                TotalExperienceYears =
                    request.TotalExperienceYears,

                ProfileSummary =
                    request.ProfileSummary,

                IsOpenToWork =
                    request.IsOpenToWork
            };

        await _candidateProfileService
            .UpdateProfileAsync(
                userId,
                model);

        return Ok();
    }

    [HttpPost("resume")]
    public async Task<IActionResult> UploadResume(
    IFormFile resume)
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        await _candidateProfileService
            .UploadResumeAsync(
                userId,
                resume);

        return NoContent();
    }

    [HttpGet("recommended-jobs")]
    public async Task<ActionResult<List<RecommendedJobModel>>> GetRecommendedJobs()
    {
        var userIdClaim =
            User.FindFirst(
                ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId =
            Guid.Parse(
                userIdClaim.Value);

        var result =
            await _candidateProfileService
                .GetRecommendedJobsAsync(
                    userId);

        return Ok(result);
    }
}