using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers.Education
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService
            _universityService;

        public UniversityController(
            IUniversityService universityService)
        {
            _universityService =
                universityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniversityResponseModel>>>
            GetAll()
        {
            var result =
                await _universityService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<UniversityResponseModel>>>
            Search(
                [FromQuery] string searchText)
        {
            var result =
                await _universityService.SearchAsync(
                    searchText);

            return Ok(result);
        }
    }
}
