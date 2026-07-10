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
        Task<List<CandidateEducationSpecialization>> GetByCandidateEducationEntityIdAsync(Guid candidateEducationEntityId);

        Task AddRangeAsync(IEnumerable<CandidateEducationSpecialization> specializations);

        void RemoveRange(IEnumerable<CandidateEducationSpecialization> specializations);
    }
}
