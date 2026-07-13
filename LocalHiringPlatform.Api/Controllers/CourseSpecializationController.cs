using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseSpecializationController
        : ControllerBase
    {
        private readonly ICourseSpecializationService
            _courseSpecializationService;

        public CourseSpecializationController(
            ICourseSpecializationService
                courseSpecializationService)
        {
            _courseSpecializationService =
                courseSpecializationService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<List<CourseSpecializationResponseModel>>>
            GetByCourseId(int courseId)
        {
            var result =
                await _courseSpecializationService
                    .GetByCourseIdAsync(courseId);

            return Ok(result);
        }
    }
}
