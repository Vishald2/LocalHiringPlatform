using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICandidateEducationSpecializationRepository
    {
        Task<List<CandidateEducationSpecialization>> GetByCandidateProfileIdAsync(Guid candidateProfileId,
                    int CourseId);
        Task AddAsync(CandidateEducationSpecialization candidateCourseSpecialization);

        Task AddRangeAsync(IEnumerable<CandidateEducationSpecialization> candidateEducationSpecializations);

        void RemoveRange(IEnumerable<CandidateEducationSpecialization> candidateEducationSpecializations);

        Task<List<CandidateEducationSpecialization>> GetByCandidateEducationIdAsync(Guid candidateEducationId); 
        void Update(CandidateEducationSpecialization candidateCourseSpecialization);

        void Delete(CandidateEducationSpecialization candidateCourseSpecialization);
    }
}
