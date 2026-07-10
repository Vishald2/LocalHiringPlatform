using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.Education
{
    public class CandidateEducationLookupService
            : ICandidateEducationLookupService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseSpecializationRepository _courseSpecializationRepository;
        private readonly IUniversityRepository _universityRepository;

        public CandidateEducationLookupService(
            IEducationRepository educationRepository,
            ICourseRepository courseRepository,
            ICourseSpecializationRepository courseSpecializationRepository,
            IUniversityRepository universityRepository)
        {
            _educationRepository = educationRepository;
            _courseRepository = courseRepository;
            _courseSpecializationRepository = courseSpecializationRepository;
            _universityRepository = universityRepository;
        }

        public async Task<List<CandidateEducationModel>> GetEducationsAsync()
        {
            var educations =
                await _educationRepository.GetAllAsync();

            return educations.Select(x => new CandidateEducationModel
            {
                EducationId = x.EducationId,
                Code = x.Code,
                EducationName = x.Name
            }).ToList();
        }

        public async Task<List<CourseModel>> GetCoursesAsync(int educationId)
        {
            var courses =
                await _courseRepository.GetByEducationIdAsync(educationId);

            return courses.Select(x => new CourseModel
            {
                CourseId = x.CourseId,
                EducationId = x.EducationId,
                Code = x.Code,
                Name = x.Name
            }).ToList();
        }

        public async Task<List<CourseSpecializationModel>>
            GetCourseSpecializationsAsync(int courseId)
        {
            var courseSpecializations =
                await _courseSpecializationRepository
                    .GetByCourseIdAsync(courseId);

            return courseSpecializations.Select(x =>
                new CourseSpecializationModel
                {
                    CourseSpecializationId = x.CourseSpecializationId,
                    CourseId = x.CourseId,
                    SpecializationId = x.SpecializationId,
                    SpecializationName = x.Specialization.Name
                }).ToList();
        }

        public async Task<List<UniversityModel>>
            SearchUniversitiesAsync(string searchText)
        {
            var universities =
                await _universityRepository.SearchAsync(searchText);

            return universities.Select(x => new UniversityModel
            {
                UniversityId = x.UniversityId,
                Name = x.Name,
                City = x.City,
                State = x.State
            }).ToList();
        }
    }
}
