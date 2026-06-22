using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.API.Controllers
{

    [Authorize(Roles = "Candidate")]
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateSkillController : ControllerBase
    {
        private readonly ICandidateSkillService
            _candidateSkillService;

        public CandidateSkillController(
            ICandidateSkillService candidateSkillService)
        {
            _candidateSkillService =
                candidateSkillService;
        }

        [HttpGet("my")]
        public async Task<IActionResult>
            GetMySkills()
        {
            Guid userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            var result =
                await _candidateSkillService
                    .GetMySkillsAsync(
                        userId);

            return Ok(
                result.Select(x =>
                    new CandidateSkillResponseDto
                    {
                        SkillId =
                            x.SkillId,

                        SkillName =
                            x.SkillName,

                        IndustryType =
                            x.IndustryType,

                        SkillCategory =
                            x.SkillCategory,

                        ExperienceInMonths =
                            x.ExperienceInMonths
                    }));
        }

        [HttpPost("my")]
        public async Task<IActionResult> SaveMySkills(
                SaveCandidateSkillsRequestDto request)
        {
            Guid userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            await _candidateSkillService
                .SaveMySkillsAsync(
                    userId,
                    request.SkillIds);

            return Ok();
        }
    }
}