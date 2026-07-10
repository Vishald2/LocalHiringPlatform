using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalHiringPlatform.Domain.Models.Education;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICandidateEducationLookupService
    {
        Task<List<CandidateEducationModel>> GetEducationsAsync();

        Task<List<CourseModel>> GetCoursesAsync(int educationId);

        Task<List<CourseSpecializationModel>> GetCourseSpecializationsAsync(int courseId);

        Task<List<UniversityModel>> SearchUniversitiesAsync(string searchText);
    }
}
