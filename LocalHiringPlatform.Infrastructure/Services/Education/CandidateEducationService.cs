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
        private readonly ICandidateEducationSpecializationRepository _candidateEducationSpecializationRepository;
        private readonly ICourseSpecializationRepository _courseSpecializationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateProfileRepository _candidateProfileRepository;

        public CandidateEducationService(
            ICandidateEducationRepository candidateEducationRepository,
            ICandidateEducationSpecializationRepository candidateEducationSpecializationRepository,
            ICourseSpecializationRepository courseSpecializationRepository,
            ICandidateProfileRepository candidateProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateEducationRepository = candidateEducationRepository;
            _candidateEducationSpecializationRepository = candidateEducationSpecializationRepository;
            _courseSpecializationRepository = courseSpecializationRepository;
            _candidateProfileRepository = candidateProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddCandidateEducationAsync(
            Guid userId,
            CandidateEducationModel model)
        {

            Guid candidateProfileId =
     _candidateProfileRepository.GetByUserIdAsync(userId).Result.EntityId;

            // Get valid CourseSpecializations
            List<CourseSpecialization> courseSpecializations = new();

            if (model.CourseSpecializationIds.Any())
            {
                courseSpecializations =
                    await _courseSpecializationRepository
                        .GetByIdsAsync(model.CourseSpecializationIds);
            }

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

                EducationId = model.EducationId,
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

            if (courseSpecializations.Any())
            {
                var candidateEducationSpecializations =
                    courseSpecializations.Select(x =>
                        new CandidateEducationSpecialization
                        {
                            CandidateEducationEntityId = candidateEducation.EntityId,
                            CourseSpecializationId = x.CourseSpecializationId
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

          //  await _candidateEducationRepository.DeleteAsync(candidateEducation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CandidateEducationModel?> GetCandidateEducationAsync(
            Guid candidateEducationEntityId)
        {
            var education =
                await _candidateEducationRepository
                    .GetByEntityIdAsync(candidateEducationEntityId);

            if (education == null)
                return null;

            return new CandidateEducationModel
            {
                EntityId = education.EntityId,

                EducationId = education.EducationId,
                EducationName = education.Education.Name,

                CourseId = education.CourseId,
                CourseName = education.Course.Name,

                UniversityId = education.UniversityId,
                UniversityName = education.University?.Name,

                InstituteName = education.InstituteName,

                StartYear = education.StartYear,
                EndYear = education.EndYear,

                Percentage = education.Percentage,
                CGPA = education.CGPA,
                Grade = education.Grade,

                IsCompleted = education.IsCompleted,
                IsHighestEducation = education.IsHighestEducation,

                CourseSpecializationIds = education
                    .CandidateEducationSpecializations
                    .Select(x => x.CourseSpecializationId)
                    .ToList(),

                SpecializationNames = education
                    .CandidateEducationSpecializations
                    .Select(x => x.CourseSpecialization.Specialization.Name)
                    .ToList()
            };
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

                EducationId = x.EducationId,
                EducationName = x.Education.Name,

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
                    .CandidateEducationSpecializations
                    .Select(s => s.CourseSpecializationId)
                    .ToList(),

                SpecializationNames = x
                    .CandidateEducationSpecializations
                    .Select(s => s.CourseSpecialization.Specialization.Name)
                    .ToList()

            }).ToList();
        }

        public async Task UpdateCandidateEducationAsync(
            Guid candidateEducationEntityId,
            CandidateEducationModel model)
        {
            var candidateEducation =
                await _candidateEducationRepository
                    .GetByEntityIdAsync(candidateEducationEntityId);

            if (candidateEducation == null)
                throw new BusinessException("Candidate education not found.");

            // Validate selected Course Specializations
            List<CourseSpecialization> courseSpecializations = new();

            if (model.CourseSpecializationIds.Any())
            {
                courseSpecializations =
                    await _courseSpecializationRepository
                        .GetByIdsAsync(model.CourseSpecializationIds);
            }

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
            candidateEducation.EducationId = model.EducationId;
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

            // Replace Specializations
            _candidateEducationSpecializationRepository.RemoveRange(
                candidateEducation.CandidateEducationSpecializations);

            if (courseSpecializations.Any())
            {
                var candidateEducationSpecializations =
                    courseSpecializations.Select(x =>
                        new CandidateEducationSpecialization
                        {
                            CandidateEducationEntityId = candidateEducation.EntityId,
                            CourseSpecializationId = x.CourseSpecializationId
                        });

                await _candidateEducationSpecializationRepository
                    .AddRangeAsync(candidateEducationSpecializations);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
