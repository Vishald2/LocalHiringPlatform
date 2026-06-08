using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/master/skill")]
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            List<SkillModel> skillModel = await _skillService.GetAllSkillsAsync();

            return Ok(
                skillModel.Select(s => new SkillResponseDto
                {
                    SkillName = s.SkillName,
                    SkillCategory = s.SkillCategory,
                    IsApproved = s.IsApproved
                })
                );
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(SkillRequestDto skillRequestDto)
        {
           await _skillService.AddSkillAsync(
                new SkillModel {
                 SkillName=skillRequestDto.SkillName,
                  IsApproved=skillRequestDto.IsApproved,
                   SkillCategory=skillRequestDto.SkillCategory
                });

            return Ok();
        }
    }
}
