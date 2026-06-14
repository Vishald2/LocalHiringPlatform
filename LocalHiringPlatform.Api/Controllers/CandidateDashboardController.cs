using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Candidate")]
    public class CandidateDashboardController
        : ControllerBase
    {
        private readonly
            ICandidateDashboardService
            _candidateDashboardService;

        public CandidateDashboardController(
            ICandidateDashboardService
                candidateDashboardService)
        {
            _candidateDashboardService =
                candidateDashboardService;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetDashboard()
        {
            Guid userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            var model =
                await _candidateDashboardService
                    .GetDashboardAsync(userId);

            var response =
                new CandidateDashboardResponseDto
                {
                    TotalApplications =
                        model.TotalApplications,

                    Shortlisted =
                        model.Shortlisted,

                    InterviewScheduled =
                        model.InterviewScheduled,

                    Rejected =
                        model.Rejected,

                    Hired =
                        model.Hired
                };

            return Ok(response);
        }
    }
}
