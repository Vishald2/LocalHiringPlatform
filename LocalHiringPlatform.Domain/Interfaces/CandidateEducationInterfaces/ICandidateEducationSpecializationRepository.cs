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
        Task<List<CandidateCourseSpecialization>> GetByCandidateEducationEntityIdAsync(Guid candidateEducationEntityId);

        Task AddRangeAsync(IEnumerable<CandidateCourseSpecialization> specializations);

        void RemoveRange(IEnumerable<CandidateCourseSpecialization> specializations);
    }
}
