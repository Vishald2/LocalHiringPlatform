using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ICandidateSkillService
    {
        Task<List<CandidateSkillModel>>
            GetMySkillsAsync(
                Guid userId);

        Task SaveMySkillsAsync(
            Guid userId,
            List<Guid> skillIds);
    }
}
