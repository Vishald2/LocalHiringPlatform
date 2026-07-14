using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.Experience;
using LocalHiringPlatform.Domain.Models.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.Experience
{
    public class CandidateExperienceService: ICandidateExperienceService
    {
        private readonly ICandidateProfileRepository
    _candidateProfileRepository;

        private readonly ICandidateExperienceRepository
            _candidateExperienceRepository;

        private readonly IUnitOfWork
            _unitOfWork;

        public CandidateExperienceService(
            ICandidateProfileRepository candidateProfileRepository,
            ICandidateExperienceRepository candidateExperienceRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateProfileRepository =
                candidateProfileRepository;

            _candidateExperienceRepository =
                candidateExperienceRepository;

            _unitOfWork =
                unitOfWork;
        }

        public async Task AddAsync(
     Guid userId,
     CandidateExperienceCreateModel model)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var candidateExperience =
                new CandidateExperience
                {
                    CandidateProfileId = candidateProfile.EntityId,

                    CompanyName = model.CompanyName,

                    Designation = model.Designation,

                    IndustryTypeId = model.IndustryTypeId,

                    City = model.City,

                    State = model.State,

                    Country = model.Country,

                    StartDate = model.StartDate,

                    EndDate = model.EndDate,

                    IsCurrentCompany = model.IsCurrentCompany,

                    Summary = model.Summary
                };

            await _candidateExperienceRepository
                .AddAsync(candidateExperience);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            Guid userId,
            Guid candidateExperienceEntityId)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var candidateExperience =
                await _candidateExperienceRepository
                    .GetByIdAsync(candidateExperienceEntityId);

            if (candidateExperience == null ||
                candidateExperience.CandidateProfileId !=
                    candidateProfile.EntityId)
            {
                throw new BusinessException(
                    "Experience not found.");
            }

            _candidateExperienceRepository
                .Remove(candidateExperience);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CandidateExperienceResponseModel>>
            GetCandidateExperiencesAsync(
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

            var experiences =
                await _candidateExperienceRepository
                    .GetByCandidateProfileIdAsync(
                        candidateProfile.EntityId);

            return experiences
                .Select(x => new CandidateExperienceResponseModel
                {
                    EntityId = x.EntityId,

                    CompanyName = x.CompanyName,

                    Designation = x.Designation,

                    IndustryTypeId = x.IndustryTypeId,

                    IndustryTypeName =
                        x.IndustryType?.Name ?? string.Empty,

                    City = x.City,

                    State = x.State,

                    Country = x.Country,

                    StartDate = x.StartDate,

                    EndDate = x.EndDate,

                    IsCurrentCompany = x.IsCurrentCompany,

                    Summary = x.Summary
                })
                .ToList();
        }

        public async Task<CandidateExperienceCreateModel?> GetDetailAsync(
            Guid userId,
            Guid candidateExperienceEntityId)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var experience =
                await _candidateExperienceRepository
                    .GetDetailAsync(candidateExperienceEntityId);

            if (experience == null ||
                experience.CandidateProfileId != candidateProfile.EntityId)
            {
                throw new BusinessException(
                    "Experience not found.");
            }

            return new CandidateExperienceCreateModel
            {
                EntityId = experience.EntityId,

                CompanyName = experience.CompanyName,

                Designation = experience.Designation,

                IndustryTypeId = experience.IndustryTypeId,

                City = experience.City,

                State = experience.State,

                Country = experience.Country,

                StartDate = experience.StartDate,

                EndDate = experience.EndDate,

                IsCurrentCompany = experience.IsCurrentCompany,

                Summary = experience.Summary
            };
        }

        public async Task UpdateAsync(
            Guid userId,
            CandidateExperienceCreateModel model)
        {
            if (model.EntityId == null)
            {
                throw new BusinessException(
                    "Experience Id is required.");
            }

            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var candidateExperience =
                await _candidateExperienceRepository
                    .GetByIdAsync(model.EntityId.Value);

            if (candidateExperience == null ||
                candidateExperience.CandidateProfileId !=
                    candidateProfile.EntityId)
            {
                throw new BusinessException(
                    "Experience not found.");
            }

            candidateExperience.CompanyName =
                model.CompanyName;

            candidateExperience.Designation =
                model.Designation;

            candidateExperience.IndustryTypeId =
                model.IndustryTypeId;

            candidateExperience.City =
                model.City;

            candidateExperience.State =
                model.State;

            candidateExperience.Country =
                model.Country;

            candidateExperience.StartDate =
                model.StartDate;

            candidateExperience.EndDate =
                model.EndDate;

            candidateExperience.IsCurrentCompany =
                model.IsCurrentCompany;

            candidateExperience.Summary =
                model.Summary;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
