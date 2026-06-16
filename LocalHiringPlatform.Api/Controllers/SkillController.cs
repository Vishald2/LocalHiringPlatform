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

            var skillResponseDto = skillModel.Select(s => new SkillResponseDto
            {
                EntityId = s.EntityId,
                SkillName = s.SkillName,
                SkillCategory = s.SkillCategory,
                IsApproved = s.IsApproved
            }).ToList();

            return Ok(skillResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(SkillRequestDto skillRequestDto)
        {
           await _skillService.AddSkillAsync(
                new SkillModel {
                 EntityId = Guid.NewGuid(),
                 SkillName=skillRequestDto.SkillName,
                  IsApproved=skillRequestDto.IsApproved,
                   SkillCategory=skillRequestDto.SkillCategory
                });

            return Ok();
        }
    }
}
