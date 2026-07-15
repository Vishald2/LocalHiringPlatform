using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers.MasterData
{
    [ApiController]
    [Route("api/master")]
    public class MasterController : ControllerBase
    {
        private readonly IIndustryTypeService _industryTypeService;
        private readonly IEducationService _educationService;
        private readonly ICourseService _courseService;
        private readonly ISkillService _skillService;

        public MasterController(
            IIndustryTypeService industryTypeService,
            IEducationService educationService,
            ICourseService courseService,
            ISkillService skillService)
        {
            _industryTypeService = industryTypeService;
            _educationService = educationService;
            _courseService = courseService;
            _skillService = skillService;
        }

        [HttpGet("industrytypes")]
        public async Task<IActionResult> GetIndustryTypes()
        {
            return Ok(
                await _industryTypeService.GetAllAsync());
        }

        //[HttpGet("educations")]
        //public async Task<IActionResult> GetEducations()
        //{
        //    return Ok(
        //        await _educationService.GetAllAsync());
        //}

        //[HttpGet("courses")]
        //public async Task<IActionResult> GetCourses()
        //{
        //    return Ok(
        //        await _courseService.GetAllAsync());
        //}

        //[HttpGet("skills")]
        //public async Task<IActionResult> GetSkills()
        //{
        //    return Ok(
        //        await _skillService.GetAllAsync());
        //}
    }
}
