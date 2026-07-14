using LocalHiringPlatform.Domain.Models.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.Experience
{
    public interface ICandidateExperienceService
    {
        Task AddAsync(
            Guid userId,
            CandidateExperienceCreateModel model);

        Task UpdateAsync(
            Guid userId,
            CandidateExperienceCreateModel model);

        Task DeleteAsync(
            Guid userId,
            Guid candidateExperienceEntityId);

        Task<List<CandidateExperienceResponseModel>>
            GetCandidateExperiencesAsync(
                Guid userId);

        Task<CandidateExperienceCreateModel?>
            GetDetailAsync(
                Guid userId,
                Guid candidateExperienceEntityId);
    }
}
