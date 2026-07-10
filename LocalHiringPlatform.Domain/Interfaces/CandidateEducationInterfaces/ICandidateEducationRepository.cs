using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
        public interface ICandidateEducationRepository
        {
            Task<List<CandidateEducation>> GetByCandidateProfileIdAsync(Guid candidateProfileId);

            Task<CandidateEducation?> GetByEntityIdAsync(Guid candidateEducationEntityId);

            Task AddAsync(CandidateEducation candidateEducation);

            void Update(CandidateEducation candidateEducation);

            void Delete(CandidateEducation candidateEducation);
        }
    }
