using DocumentFormat.OpenXml.Spreadsheet;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using LocalHiringPlatform.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.Education
{
    public class CandidateEducationService : ICandidateEducationService
    {
        private readonly ICandidateEducationRepository _candidateEducationRepository;
        private readonly ICourseSpecializationRepository _courseSpecializationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly ICandidateEducationSpecializationRepository _candidateEducationSpecializationRepository;

        public CandidateEducationService(
            ICandidateEducationRepository candidateEducationRepository,
            ICourseSpecializationRepository courseSpecializationRepository,
            ICandidateProfileRepository candidateProfileRepository,
            ICandidateEducationSpecializationRepository candidateEducationSpecializationRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateEducationRepository = candidateEducationRepository;
            _courseSpecializationRepository = courseSpecializationRepository;
            _candidateProfileRepository = candidateProfileRepository;
            _candidateEducationSpecializationRepository = candidateEducationSpecializationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CandidateEducationModel>> GetCandidateEducationsAsync(
                Guid userId)
        {

            Guid candidateProfileId =
                    _candidateProfileRepository
                    .GetByUserIdAsync(userId)
                    .Result.EntityId;

            var educations =
                await _candidateEducationRepository
                    .GetByCandidateProfileIdAsync(candidateProfileId);

            return educations.Select(x => new CandidateEducationModel
            {
                EntityId = x.EntityId,

                EducationId = x.Course.EducationId,
                EducationName = x.Course.Education.Name,

                Code = x.Course.Education.Code,

                CourseId = x.CourseId,
                CourseName = x.Course.Name,

                UniversityId = x.UniversityId,
                UniversityName = x.University?.Name,

                InstituteName = x.InstituteName,

                StartYear = x.StartYear,
                EndYear = x.EndYear,

                Percentage = x.Percentage,
                CGPA = x.CGPA,
                Grade = x.Grade,

                IsCompleted = x.IsCompleted,
                IsHighestEducation = x.IsHighestEducation,

                CourseSpecializationIds = x
                    .CandidateCourseSpecializations
                    .Select(s => s.SpecializationId)
                    .ToList(),

                SpecializationNames = x
                    .CandidateCourseSpecializations
                    .Select(s => s.Specialization.Name)
                    .ToList()

            }).ToList();
        }

        public async Task<CandidateEducationModel?> GetCandidateEducationAsync(
                Guid candidateEducationEntityId)
        {
            var candidateEducation =
                await _candidateEducationRepository
                    .GetByEntityIdAsync(candidateEducationEntityId);

            if (candidateEducation == null)
                return null;

            return new CandidateEducationModel
            {
                CGPA = candidateEducation.CGPA,
                CourseId = candidateEducation.CourseId,
                CourseName = candidateEducation.Course.Name,
                EndYear = candidateEducation.EndYear,
                EntityId = candidateEducation.EntityId,
                Grade = candidateEducation.Grade,
                InstituteName = candidateEducation.InstituteName,
                IsCompleted = candidateEducation.IsCompleted,
                IsHighestEducation = candidateEducation.IsHighestEducation,
                EducationName=candidateEducation.Course.Education.Name,
                Code = candidateEducation.Course.Education.Code,
                EducationId = candidateEducation.Course.EducationId,
                Percentage = candidateEducation.Percentage,
                StartYear = candidateEducation.StartYear,
                UniversityId = candidateEducation?.UniversityId,
                SpecializationNames = candidateEducation?.CandidateCourseSpecializations
                    .Select(s => s.Specialization.Name)
                    .ToList(),
                CourseSpecializationIds = candidateEducation?.CandidateCourseSpecializations
                    .Select(s => s.SpecializationId)
                    .ToList()
            };
        }



        public async Task AddCandidateEducationAsync(
    Guid userId,
    CandidateEducationCreateModel model)
        {

            if (model.Percentage.HasValue &&
                (model.Percentage < 0 || model.Percentage > 100))
            {
                throw new BusinessException(
                    "Percentage must be between 0 and 100.");
            }

            if (model.CGPA.HasValue &&
                (model.CGPA < 0 || model.CGPA > 10))
            {
                throw new BusinessException(
                    "CGPA must be between 0 and 10.");
            }

            Guid candidateProfileId =
                    _candidateProfileRepository
                    .GetByUserIdAsync(userId)
                    .Result.EntityId;

            // Ensure only one Highest Education
            if (model.IsHighestEducation)
            {
                var candidateEducations =
                    await _candidateEducationRepository
                        .GetByCandidateProfileIdAsync(candidateProfileId);

                foreach (var education in candidateEducations)
                {
                    education.IsHighestEducation = false;

                    _candidateEducationRepository.Update(education);
                }
            }

            var candidateEducation = new CandidateEducation
            {
                CandidateProfileId = candidateProfileId,
            
                CourseId = model.CourseId,
                UniversityId = model.UniversityId,

                InstituteName = model.InstituteName,

                StartYear = model.StartYear,
                EndYear = model.EndYear,

                Percentage = model.Percentage,
                CGPA = model.CGPA,
                Grade = model.Grade,

                IsCompleted = model.IsCompleted,
                IsHighestEducation = model.IsHighestEducation
            };

            await _candidateEducationRepository.AddAsync(candidateEducation);

            // Get valid CourseSpecializations
            List<CourseSpecialization> courseSpecializations = new();

            if (model.CourseSpecializationIds.Any())
            {
                courseSpecializations =
                    await _courseSpecializationRepository
                        .GetByIdsAsync(model.CourseSpecializationIds);
            }

            /*ADDING COURSE SPECIALIZATIONS*/
            if (courseSpecializations.Any())
            {
                var candidateEducationSpecializations =
                    courseSpecializations.Select(x =>
                        new CandidateEducationSpecialization
                        {
                            CandidateEducationEntityId = candidateEducation.EntityId,
                            SpecializationId = x.CourseSpecializationId
                        });

                await _candidateEducationSpecializationRepository
                    .AddRangeAsync(candidateEducationSpecializations);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCandidateEducationAsync(Guid candidateEducationEntityId)
        {
            var candidateEducation = await _candidateEducationRepository.GetByEntityIdAsync(candidateEducationEntityId);

            if (candidateEducation == null)
                throw new InvalidOperationException("Candidate education not found");

            _candidateEducationRepository.Delete(candidateEducation);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task UpdateCandidateEducationAsync(
            Guid candidateEducationEntityId,
            CandidateEducationCreateModel model)
        {
            if (model.Percentage.HasValue &&
    (model.Percentage < 0 || model.Percentage > 100))
            {
                throw new BusinessException(
                    "Percentage must be between 0 and 100.");
            }

            if (model.CGPA.HasValue &&
                (model.CGPA < 0 || model.CGPA > 10))
            {
                throw new BusinessException(
                    "CGPA must be between 0 and 10.");
            }

            var candidateEducation =
                await _candidateEducationRepository
                    .GetByEntityIdAsync(candidateEducationEntityId);

            if (candidateEducation == null)
                throw new BusinessException("Candidate education not found.");

            // Ensure only one Highest Education
            if (model.IsHighestEducation)
            {
                var candidateEducations =
                    await _candidateEducationRepository
                        .GetByCandidateProfileIdAsync(candidateEducation.CandidateProfileId);

                foreach (var education in candidateEducations)
                {
                    if (education.EntityId != candidateEducation.EntityId)
                    {
                        education.IsHighestEducation = false;
                    }
                }
            }

            // Update fields
         //   candidateEducation.EducationId = model.EducationId;
            candidateEducation.CourseId = model.CourseId;
            candidateEducation.UniversityId = model.UniversityId;
            candidateEducation.InstituteName = model.InstituteName;
            candidateEducation.StartYear = model.StartYear;
            candidateEducation.EndYear = model.EndYear;
            candidateEducation.Percentage = model.Percentage;
            candidateEducation.CGPA = model.CGPA;
            candidateEducation.Grade = model.Grade;
            candidateEducation.IsCompleted = model.IsCompleted;
            candidateEducation.IsHighestEducation = model.IsHighestEducation;

            var existingSpecializations =
                await _candidateEducationSpecializationRepository
                    .GetByCandidateEducationIdAsync(candidateEducationEntityId);

            _candidateEducationSpecializationRepository.RemoveRange(
                existingSpecializations);

            // Validate selected Course Specializations
            List<CourseSpecialization> courseSpecializations = new();

            if (model.CourseSpecializationIds.Any())
            {
                courseSpecializations =
                    await _courseSpecializationRepository
                        .GetByIdsAsync(model.CourseSpecializationIds);
            }

            if (courseSpecializations.Any())
            {
                var candidateEducationSpecializations =
                    courseSpecializations.Select(x =>
                        new  CandidateEducationSpecialization
                        {
                            CandidateEducationEntityId = candidateEducation.EntityId,
                            SpecializationId = x.CourseSpecializationId
                        });

                await _candidateEducationSpecializationRepository
                    .AddRangeAsync(candidateEducationSpecializations);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
