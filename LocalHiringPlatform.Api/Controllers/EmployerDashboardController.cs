using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Employer")]
    public class EmployerDashboardController : ControllerBase
    {
        private readonly IEmployerDashboardService
            _employerDashboardService;

        public EmployerDashboardController(
            IEmployerDashboardService employerDashboardService)
        {
            _employerDashboardService =
                employerDashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
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

            var model =
                await _employerDashboardService
                    .GetDashboardAsync(userId);

            var response =
                new EmployerDashboardResponseDto
                {
                    TotalJobs =
                        model.TotalJobs,

                    ActiveJobs =
                        model.ActiveJobs,

                    TotalApplicants =
                        model.TotalApplicants,

                    NewApplications =
                        model.NewApplications
                };

            return Ok(response);
        }
    }
}