using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/job")]
public class JobController
    : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(
        IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<IActionResult> AddJob(
        CreateJobRequestDto dto)
    {
        var claims = User.Claims.ToList();

        Guid userId =
            Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value);

        await _jobService.AddJobAsync(
            new CreateJobModel
            {
                Title = dto.Title,
                Description = dto.Description,
                City = dto.City,
                State = dto.State,
                MinSalary = dto.MinSalary,
                MaxSalary = dto.MaxSalary,
                ExperienceRequired =
                    dto.ExperienceRequired,
                RequiredSkills =
                    dto.RequiredSkills
            },
            userId);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobs =
            await _jobService
                .GetAllJobsAsync();

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(
    Guid id)
    {
        var job =
            await _jobService
                .GetJobAsync(id);

        if (job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }
}