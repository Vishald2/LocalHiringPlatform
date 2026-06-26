using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/jobapplication")]
public class JobApplicationController
    : ControllerBase
{
    private readonly
        IJobApplicationService
        _jobApplicationService;

    public JobApplicationController(
        IJobApplicationService
            jobApplicationService)
    {
        _jobApplicationService =
            jobApplicationService;
    }

    [HttpPost]
    public async Task<IActionResult> ApplyToJob(
            ApplyToJobRequestDto dto)
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        await _jobApplicationService
            .ApplyToJobAsync(
                new ApplyToJobModel
                {
                    JobId = dto.JobId
                },
                userId);

        return Ok();
    }

    [HttpGet("candidate/my")]
    public async Task<IActionResult> GetMyApplications()
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        var result =
            await _jobApplicationService
                .GetMyApplicationsAsync(
                    userId);

        return Ok(result);
    }

    [HttpGet("job/{jobId}")] 
    public async Task<IActionResult> GetApplicants(Guid jobId)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result =
            await _jobApplicationService.GetApplicantsAsync(jobId, userId);

        return Ok(result);
    }

    [HttpGet("employer/my")]
    public async Task<IActionResult>GetAllApplicantsByEmployerProfile()
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        var model =
    await _jobApplicationService
        .GetAllApplicantsByEmployerProfile(
            userId);

        var response =
    model.Select(x =>
        new ApplicantResponseDto
        {
            JobApplicationId = x.JobApplicationId,
            CandidateProfileId =  x.CandidateProfileId,

            CandidateName = x.CandidateName,

            Email =  x.Email,

            MobileNumber =   x.MobileNumber,

            JobTitle = x.JobTitle,
            
            JobId =  x.JobId,

            AppliedOn = x.AppliedOn,

            Status = x.Status,

            ResumeFileName = x.ResumeFileName,

            ResumeFilePath = x.ResumeFilePath,

            MatchPercentage = x.MatchPercentage,

            Skills = x.Skills

        }).ToList();

        return Ok(response);
    }

    [HttpPut("status")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult>
    UpdateStatus(UpdateApplicationStatusRequestDto request)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var model = new UpdateApplicationStatusModel
            {
                JobApplicationId = request.JobApplicationId,

                Status = request.Status
            };

        await _jobApplicationService
            .UpdateStatusAsync(
                model,
                userId);

        return NoContent();
    }
}