using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICandidateCourseSpecializationRepository
    {
        Task<List<CandidateCourseSpecialization>> GetByCandidateProfileIdAsync(Guid candidateProfileId,
                    int CourseId);
        Task AddAsync(CandidateCourseSpecialization candidateCourseSpecialization);

        void Update(CandidateCourseSpecialization candidateCourseSpecialization);

        void Delete(CandidateCourseSpecialization candidateCourseSpecialization);
    }
}
