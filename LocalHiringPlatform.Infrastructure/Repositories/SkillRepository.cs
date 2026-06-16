using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        ApplicationDbContext _dbContext;
        public SkillRepository( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Skill skill)
        {
            _dbContext.Skills.Add(skill);
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
           return  _dbContext.Skills.ToList();
        }

        public async Task<Skill?> GetSkillAsync(Guid EntityId)
        {
           return await _dbContext.Skills.Where(s => s.EntityId == EntityId).FirstOrDefaultAsync();
        }
    }
}
