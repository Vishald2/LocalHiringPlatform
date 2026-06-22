using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using LocalHiringPlatform.Domain.Models;
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

        public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
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

           await _skillRepository.AddAsync(skill);
           await  _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<SkillModel>> GetAllSkillsAsync()
        {

            List<Skill> skills = await _skillRepository.GetAllSkillsAsync();

            var skillModel = skills.Select(skill => new SkillModel
            {
                EntityId = skill.EntityId,
                SkillName = skill.SkillName,
                SkillCategory = skill.SkillCategory,
                IsApproved = skill.IsApproved,
                IndustryType=skill.IndustryType

            }).ToList();

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
