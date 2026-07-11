using LocalHiringPlatform.Domain.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetByEducationIdAsync(int educationId);
    }
}
