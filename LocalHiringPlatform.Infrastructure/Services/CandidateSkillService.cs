using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class CandidateSkillService
    : ICandidateSkillService
    {
        private readonly ICandidateSkillRepository
            _candidateSkillRepository;

        private readonly ICandidateProfileRepository
            _candidateProfileRepository;

        private readonly IUnitOfWork
            _unitOfWork;

        public CandidateSkillService(
            ICandidateSkillRepository candidateSkillRepository,
            ICandidateProfileRepository candidateProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateSkillRepository =
                candidateSkillRepository;

            _candidateProfileRepository =
                candidateProfileRepository;

            _unitOfWork =
                unitOfWork;
        }

        public async Task<List<CandidateSkillModel>>
            GetMySkillsAsync(
                Guid userId)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var skills =
                await _candidateSkillRepository
                    .GetByCandidateProfileIdAsync(
                        candidateProfile.EntityId);

            return skills
                .Select(x =>
                    new CandidateSkillModel
                    {
                        SkillId =
                            x.SkillId,

                        SkillName =
                            x.Skill.SkillName,

                        IndustryType =
                            x.Skill.IndustryType,

                        SkillCategory =
                            x.Skill.SkillCategory
                                .ToString(),

                        ExperienceInMonths =
                            x.ExperienceInMonths
                    })
                .ToList();
        }

        public async Task SaveMySkillsAsync(
            Guid userId,
            List<Guid> skillIds)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var existingSkills =
                await _candidateSkillRepository
                    .GetByCandidateProfileIdAsync(
                        candidateProfile.EntityId);

            await _candidateSkillRepository
                .RemoveRangeAsync(
                    existingSkills);

            var candidateSkills =
                skillIds
                    .Select(skillId =>
                        new CandidateSkill
                        {
                            CandidateProfileId =
                                candidateProfile.EntityId,

                            SkillId =
                                skillId
                        })
                    .ToList();

            await _candidateSkillRepository
                .AddRangeAsync(
                    candidateSkills);

            await _unitOfWork
                .SaveChangesAsync();
        }
    }
}
