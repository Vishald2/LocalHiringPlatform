using LocalHiringPlatform.Domain.Entities.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.Experience
{
    public interface ICandidateExperienceRepository
        : IRepository<CandidateExperience>
    {
        Task<List<CandidateExperience>> GetByCandidateProfileIdAsync(
            Guid candidateProfileId);

        Task<CandidateExperience?> GetDetailAsync(
            Guid candidateExperienceEntityId);
    }
}
