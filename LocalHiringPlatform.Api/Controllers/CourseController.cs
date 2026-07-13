using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(
        ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("education/{educationId}")]
    public async Task<ActionResult<List<CourseModel>>> GetByEducationId(
        int educationId)
    {
        var courses =
            await _courseService.GetByEducationIdAsync(
                educationId);

        return Ok(courses);
    }
}