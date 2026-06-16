using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.MasterDataServices
{
    public interface ISkillService
    {
        Task<SkillModel?>GetSkillAsync(Guid EntityId);

        Task<List<SkillModel>> GetAllSkillsAsync();

        Task AddSkillAsync(SkillModel skillModel);
    }
}
