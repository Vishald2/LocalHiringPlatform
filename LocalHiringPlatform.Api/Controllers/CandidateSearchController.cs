using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Employer")]
    public class CandidateSearchController
        : ControllerBase
    {
        private readonly
            ICandidateSearchService
            _candidateSearchService;

        public CandidateSearchController(
            ICandidateSearchService
                candidateSearchService)
        {
            _candidateSearchService =
                candidateSearchService;
        }

        [HttpPost]
        public async Task<IActionResult>
            Search(
                CandidateSearchRequestDto request)
        {
            var model =
                new CandidateSearchModel
                {
                    Name =
                        request.Name,

                    City =
                        request.City,

                    SkillId =
                        request.SkillId
                };

            var result =
                await _candidateSearchService
                    .SearchAsync(model);

            var response =
                result.Select(x =>
                    new CandidateSearchResponseDto
                    {
                        CandidateProfileId =
                            x.CandidateProfileId,

                        FullName =
                            x.FullName,

                        Email =
                            x.Email,

                        MobileNumber =
                            x.MobileNumber,

                        City =
                            x.City,

                        TotalExperienceYears =
                            x.TotalExperienceYears,

                        ResumeFileName =
                            x.ResumeFileName,

                        ResumeFilePath =
                            x.ResumeFilePath,

                        IsOpenToWork =
                            x.IsOpenToWork
                    })
                .ToList();

            return Ok(response);
        }
    }
}