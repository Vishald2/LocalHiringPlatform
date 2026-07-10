using LocalHiringPlatform.Domain.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface ICandidateEducationService
    {
        Task<List<CandidateEducationModel>> GetCandidateEducationsAsync(Guid candidateProfileId);

        Task<CandidateEducationModel?> GetCandidateEducationAsync(Guid candidateEducationEntityId);

        Task AddCandidateEducationAsync(
            Guid candidateProfileId,
            CandidateEducationModel model);

        Task UpdateCandidateEducationAsync(
            Guid candidateEducationEntityId,
            CandidateEducationModel model);

        Task DeleteCandidateEducationAsync(Guid candidateEducationEntityId);
    }
}
