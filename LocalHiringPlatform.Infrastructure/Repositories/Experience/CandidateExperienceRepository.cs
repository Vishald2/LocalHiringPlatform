using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Domain.Interfaces.Experience;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories.Experience
{
    public class CandidateExperienceRepository
                : Repository<CandidateExperience>,
                    ICandidateExperienceRepository
    {
        public CandidateExperienceRepository(
       ApplicationDbContext dbContext)
       : base(dbContext)
        {
                
        }

        public async Task<List<CandidateExperience>>
            GetByCandidateProfileIdAsync(
                Guid candidateProfileId)
                {
                    return await _dbContext.CandidateExperiences
                        .Where(x => x.CandidateProfileId == candidateProfileId)
                        .Include(x => x.IndustryType)
                        .OrderByDescending(x => x.StartDate)
                        .ToListAsync();
                }

        public async Task<CandidateExperience?> GetDetailAsync(
            Guid candidateExperienceEntityId)
        {
            return await _dbContext.CandidateExperiences
                .Where(x => x.EntityId == candidateExperienceEntityId)
                .Include(x => x.IndustryType)
                .FirstOrDefaultAsync();
        }
    }
}

