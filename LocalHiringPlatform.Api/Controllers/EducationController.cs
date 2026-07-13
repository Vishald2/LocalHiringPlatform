using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;

    public EducationController(
        IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EducationModel>>> GetAll()
    {
        var educations =
            await _educationService.GetAllAsync();

        return Ok(educations);
    }
}