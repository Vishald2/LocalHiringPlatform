using LocalHiringPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ICandidateSkillRepository
    {
        Task<List<CandidateSkill>> GetByCandidateProfileIdAsync(Guid candidateProfileId);

        Task AddRangeAsync(List<CandidateSkill> candidateSkills);

        Task RemoveRangeAsync(List<CandidateSkill> candidateSkills);

        Task<List<CandidateSkill>> GetByCandidateProfileIdsAsync(List<Guid> candidateProfileIds);
    }
}
