using LocalHiringPlatform.Api.Extensions;
using LocalHiringPlatform.Domain.Interfaces.Experience;
using LocalHiringPlatform.Domain.Models.Experience;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers.Experience
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CandidateExperienceController : ControllerBase
    {
        private readonly ICandidateExperienceService
            _candidateExperienceService;

        public CandidateExperienceController(
            ICandidateExperienceService candidateExperienceService)
        {
            _candidateExperienceService =
                candidateExperienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateExperiences()
        {
            var userId =
                User.GetUserId();

            return Ok(
                await _candidateExperienceService
                    .GetCandidateExperiencesAsync(userId));
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetDetail(
            Guid candidateExperienceEntityId)
        {
            var userId =
                User.GetUserId();

            var result = await _candidateExperienceService
                    .GetDetailAsync(
                        userId,
                        candidateExperienceEntityId);

            return Ok(result
                );
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            CandidateExperienceCreateModel model)
        {
            var userId =
                User.GetUserId();

            await _candidateExperienceService
                .AddAsync(
                    userId,
                    model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            CandidateExperienceCreateModel model)
        {
            var userId =
                User.GetUserId();

            await _candidateExperienceService
                .UpdateAsync(
                    userId,
                    model);

            return Ok();
        }

        [HttpDelete("{candidateExperienceEntityId}")]
        public async Task<IActionResult> Delete(
            Guid candidateExperienceEntityId)
        {
            var userId =
                User.GetUserId();

            await _candidateExperienceService
                .DeleteAsync(
                    userId,
                    candidateExperienceEntityId);

            return Ok();
        }
    }
}
