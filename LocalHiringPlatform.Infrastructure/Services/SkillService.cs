using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using LocalHiringPlatform.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IOptions<ApplicationSettings> _appSetting;

        public SkillService(ISkillRepository skillRepository, 
            IUnitOfWork unitOfWork, 
            IRedisCacheService redisCacheService,
            IOptions<ApplicationSettings> appSetting
            )
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
            _redisCacheService = redisCacheService;
            _appSetting = appSetting;
        }
        public async Task AddSkillAsync(SkillModel skillModel)
        {
            Skill skill = new Skill
            {
                SkillName = skillModel.SkillName,
                SkillCategory = skillModel.SkillCategory,
                IsApproved = skillModel.IsApproved,
                IndustryType=skillModel.IndustryType
            };

            if(skill.SkillName=="")
            {
                throw new ArgumentException("Skill name cannot be empty.");
            }


            await _skillRepository.AddAsync(skill);
           await  _unitOfWork.SaveChangesAsync();

            if (_appSetting.Value.UseRedis == true)
            {
                try
                {
                    await _redisCacheService.RemoveAsync("Skills");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<List<SkillModel>> GetAllSkillsAsync()
        {
            const string cacheKey = "Skills";

            if (_appSetting.Value.UseRedis == true)
            {
                var cachedSkills =
                    await _redisCacheService
                        .GetAsync<List<SkillModel>>(cacheKey);

                if (cachedSkills != null)
                {
                    return cachedSkills;
                }
            }

            List<Skill> skills = await _skillRepository.GetAllSkillsAsync();

            var skillModel = skills.Select(skill => new SkillModel
            {
                EntityId = skill.EntityId,
                SkillName = skill.SkillName,
                SkillCategory = skill.SkillCategory,
                IsApproved = skill.IsApproved,
                IndustryType=skill.IndustryType

            }).ToList();

            if (_appSetting.Value.UseRedis == true)
            {
                await _redisCacheService.SetAsync(
                    cacheKey,
                    skillModel,
                    TimeSpan.FromHours(1));
            }

            return skillModel;
        }

        public async Task<SkillModel?> GetSkillAsync(Guid EntityId)
        {
            Skill? skill = await _skillRepository.GetSkillAsync(EntityId);

            if (skill is not null)
            {
                return new SkillModel
                {
                    EntityId = skill.EntityId,
                    SkillName = skill.SkillName,
                    SkillCategory = skill.SkillCategory,
                    IsApproved = skill.IsApproved,
                    IndustryType=skill.IndustryType
                };
            }
            else
                return null;
        }
    }
}
