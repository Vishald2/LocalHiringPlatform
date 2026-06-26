using LocalHiringPlatform.Api.Extensions;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [Authorize(Roles = "Candidate")]
    [ApiController]
    [Route("api/[controller]")]
    public class SavedJobController : ControllerBase
    {
        private readonly ISavedJobService _savedJobService;

        public SavedJobController(
            ISavedJobService savedJobService)
        {
            _savedJobService = savedJobService;
        }

        [HttpPost("{jobId}")]
        public async Task<IActionResult> SaveJob(Guid jobId)
        {
            Guid userId = User.GetUserId();

            await _savedJobService.SaveJobAsync(userId, jobId);

            return Ok();
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMySavedJobs()
        {
            Guid userId = User.GetUserId();

            var result = await _savedJobService
                .GetMySavedJobsAsync(userId);


            var jobs = result.Select(job => new DTOs.JobResponseDto
            {
                EntityId = job.EntityId,
                Title = job.Title,
                Description = job.Description,
                City = job.City,
                State = job.State,
                MinSalary = job.MinSalary,
                MaxSalary = job.MaxSalary,
                ExperienceRequired = job.ExperienceRequired,
                RequiredSkills = job.RequiredSkills,
                IsActive = job.IsActive,
                ApplicantCount = job.ApplicantCount
            }).ToList();

            return Ok(jobs);
        }

        [HttpDelete("{jobId}")]
        public async Task<IActionResult> RemoveSavedJob(Guid jobId)
        {
            Guid userId = User.GetUserId();

            await _savedJobService
                .RemoveSavedJobAsync(userId, jobId);

            return Ok();
        }
    }
}
