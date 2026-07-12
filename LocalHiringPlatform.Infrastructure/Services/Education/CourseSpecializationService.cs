using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.Education
{
    public class CourseSpecializationService
        : ICourseSpecializationService
    {
        private readonly ICourseSpecializationRepository
            _courseSpecializationRepository;

        public CourseSpecializationService(
            ICourseSpecializationRepository
                courseSpecializationRepository)
        {
            _courseSpecializationRepository =
                courseSpecializationRepository;
        }

        public async Task<List<CourseSpecializationResponseModel>>
            GetByCourseIdAsync(
                int courseId)
        {
            var courseSpecializations =
                await _courseSpecializationRepository
                    .GetByCourseIdAsync(courseId);

            return courseSpecializations
                .Select(x => new CourseSpecializationResponseModel
                {
                    CourseSpecializationId =
                        x.CourseSpecializationId,

                    SpecializationId =
                        x.SpecializationId,

                    SpecializationName =
                        x.Specialization.Name
                })
                .ToList();
        }
    }
}
