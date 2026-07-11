using DocumentFormat.OpenXml.Spreadsheet;
using LocalHiringPlatform.Api.Extensions;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CandidateEducationController : ControllerBase
    {
        private readonly ICandidateEducationService _candidateEducationService;

        public CandidateEducationController(
            ICandidateEducationService candidateEducationService)
        {
            _candidateEducationService = candidateEducationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateEducations()
        {
            var userId = User.GetUserId();

            var result =
                await _candidateEducationService
                    .GetCandidateEducationsAsync(userId);

            return Ok(result);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> 
            GetCandidateEducationDetails(Guid candidateEducationEntityId)
        {
            var result =
                await _candidateEducationService
                    .GetCandidateEducationAsync(candidateEducationEntityId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidateEducation(
                           [FromBody] CandidateEducationCreateModel model)
        {
            var userId = User.GetUserId();

            await _candidateEducationService.AddCandidateEducationAsync(
                userId,
                model);

            return Ok();
        }

        [HttpPut("{candidateEducationEntityId:guid}")]
        public async Task<IActionResult> UpdateCandidateEducation(
            Guid candidateEducationEntityId,
            [FromBody] CandidateEducationCreateModel model)
        {
            await _candidateEducationService
                .UpdateCandidateEducationAsync(candidateEducationEntityId, model);

            return Ok();
        }

        [HttpDelete("{candidateEducationEntityId:guid}")]
        public async Task<IActionResult> DeleteCandidateEducation(
            Guid candidateEducationEntityId)
        {
            await _candidateEducationService
                .DeleteCandidateEducationAsync(candidateEducationEntityId);

            return Ok();
        }
    }
}


//using LocalHiringPlatform.Api.Extensions;
//using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
//using LocalHiringPlatform.Domain.Models.Education;
//using LocalHiringPlatform.Infrastructure.Services.Education;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace LocalHiringPlatform.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class EducationLookupController : ControllerBase
//    {
//        private readonly ICandidateEducationLookupService _lookupService;
//        private readonly ICandidateEducationService _candidateEducationService;

//        public EducationLookupController(
//            ICandidateEducationLookupService lookupService,
//            ICandidateEducationService candidateEducationService)
//        {
//            _lookupService = lookupService;
//            _candidateEducationService = candidateEducationService;
//        }

//        [HttpPost]
//       // [Authorize]
//        public async Task<IActionResult> AddCandidateEducation(
//    [FromBody] CandidateEducationModel model)
//        {
//            var userId = User.GetUserId();



//            await _candidateEducationService.AddCandidateEducationAsync(
//                userId,
//                model);

//            return Ok();
//        }

//        [HttpGet("educations")]
//        public async Task<IActionResult> GetEducations()
//        {
//            return Ok(await _lookupService.GetEducationsAsync());
//        }

//        [HttpGet("courses/{educationId}")]
//        public async Task<IActionResult> GetCourses(int educationId)
//        {
//            return Ok(await _lookupService.GetCoursesAsync(educationId));
//        }

//        [HttpGet("specializations/{courseId}")]
//        public async Task<IActionResult> GetCourseSpecializations(int courseId)
//        {
//            return Ok(await _lookupService.GetCourseSpecializationsAsync(courseId));
//        }

//        [HttpGet("universities")]
//        public async Task<IActionResult> SearchUniversities(
//            [FromQuery] string searchText)
//        {
//            return Ok(await _lookupService.SearchUniversitiesAsync(searchText));
//        }
//    }
//}