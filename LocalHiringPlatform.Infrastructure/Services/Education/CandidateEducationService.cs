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

        public CandidateEducationService(
            ICandidateEducationRepository candidateEducationRepository,
            ICourseSpecializationRepository courseSpecializationRepository,
            ICandidateProfileRepository candidateProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateEducationRepository = candidateEducationRepository;
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

               // EducationId = model.EducationId,
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

            //if (courseSpecializations.Any())
            //{
            //    var candidateEducationSpecializations =
            //        courseSpecializations.Select(x =>
            //            new CandidateCourseSpecialization
            //            {
            //              //  CandidateEducationEntityId = candidateEducation.EntityId,
            //            //    CourseSpecializationId = x.CourseSpecializationId
            //            });

            //    await _candidateEducationSpecializationRepository
            //        .AddRangeAsync(candidateEducationSpecializations);
            //}

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
                CGPA = education.CGPA,
                CourseId = education.CourseId,
                CourseName = education.Course.Name,
                EndYear = education.EndYear,
                EntityId = education.EntityId,
                Grade = education.Grade,
                InstituteName = education.InstituteName,
                IsCompleted = education.IsCompleted,
                IsHighestEducation = education.IsHighestEducation,
                EducationName=education.Course.Education.Name,
                Code = education.Course.Education.Code,
                EducationId = education.Course.EducationId,
                Percentage = education.Percentage,
                SpecializationNames = education.Course.CourseSpecializations
                    .Select(s => s.Specialization.Name)
                    .ToList(),
                StartYear = education.StartYear,
                UniversityId = education?.UniversityId,
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

                //EducationId = x.EducationId,
                //EducationName = x.Education.Name,

                //Code = x.Education.Code,

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
                    .Course.CourseSpecializations
                    .Select(s => s.SpecializationId)
                    .ToList(),

                SpecializationNames = x
                    .Course.CourseSpecializations
                    .Select(s => s.Specialization.Name)
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

            //// Replace Specializations
            //_candidateEducationSpecializationRepository.RemoveRange(
            //    candidateEducation.CandidateEducationSpecializations);

            //if (courseSpecializations.Any())
            //{
            //    var candidateEducationSpecializations =
            //        courseSpecializations.Select(x =>
            //            new CandidateCourseSpecialization
            //            {
            //                CandidateEducationEntityId = candidateEducation.EntityId,
            //                CourseSpecializationId = x.CourseSpecializationId
            //            });

            //    await _candidateEducationSpecializationRepository
            //        .AddRangeAsync(candidateEducationSpecializations);
            //}

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
