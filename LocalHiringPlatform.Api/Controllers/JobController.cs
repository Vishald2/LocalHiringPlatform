using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Exceptions;
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

    [HttpGet("myjobs")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> GetMyJobs()
    {
        var userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                    .Value);

        var jobs =
            await _jobService
                .GetEmployerJobsAsync(userId);

        return Ok(jobs);
    }

    [HttpPost]
    public async Task<IActionResult> AddJob(CreateJobRequestDto dto)
    {
        var claims = User.Claims.ToList();

        Guid userId =
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

    [HttpPut]
    public async Task<IActionResult>
    UpdateJob(UpdateJobRequestDto request)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        var model = new UpdateJobModel
            {
                EntityId = request.EntityId,

                Title = request.Title,

                Description = request.Description,

                City = request.City,

                State = request.State,

                MinSalary = request.MinSalary,

                MaxSalary = request.MaxSalary,

                ExperienceRequired = request.ExperienceRequired,

                RequiredSkills = request.RequiredSkills,

                IsActive = request.IsActive
            };

        await _jobService.UpdateJobAsync(userId, model);

        return NoContent();
    }

    [HttpPost("search")]
    public async Task<IActionResult>SearchJobs(SearchJobsModel model)
    {
        var jobs = await _jobService.SearchJobsAsync(model);

        return Ok(jobs);
    }
}