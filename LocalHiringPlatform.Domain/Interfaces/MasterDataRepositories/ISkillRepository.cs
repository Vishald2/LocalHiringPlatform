using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories
{
    public interface ISkillRepository
    {
        Task AddAsync(Skill skill);

        Task<Skill?> GetSkillAsync(Guid Id);

        Task<List<Skill>> GetAllSkillsAsync();
    }
}
