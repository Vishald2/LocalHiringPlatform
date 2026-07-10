using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICourseSpecializationRepository
    {
        Task<List<CourseSpecialization>> GetByCourseIdAsync(int courseId);

        Task<List<CourseSpecialization>> GetByIdsAsync(List<int> courseSpecializationIds);
    }
}
