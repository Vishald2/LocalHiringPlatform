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
        Task<List<CandidateEducationSpecialization>> GetByCandidateProfileIdAsync(Guid candidateProfileId,
                    int CourseId);
        Task AddAsync(CandidateEducationSpecialization candidateCourseSpecialization);

        void Update(CandidateEducationSpecialization candidateCourseSpecialization);

        void Delete(CandidateEducationSpecialization candidateCourseSpecialization);
    }
}
