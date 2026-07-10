using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetByEducationIdAsync(int educationId);

        Task<Course?> GetByIdAsync(int courseId);
    }
}
