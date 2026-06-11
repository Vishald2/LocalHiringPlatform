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
    public async Task<IActionResult>
        ApplyToJob(
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

    [HttpGet("my")]
    public async Task<IActionResult>
    GetMyApplications()
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
    public async Task<IActionResult>
    GetApplicants(
        Guid jobId)
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        var result =
            await _jobApplicationService
                .GetApplicantsAsync(
                    jobId,
                    userId);

        return Ok(result);
    }
}