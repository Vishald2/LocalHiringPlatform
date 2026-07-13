using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories.EducationRepositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EducationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await _dbContext.Educations
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Education?> GetByIdAsync(int educationId)
        {
            return await _dbContext.Educations
                .FirstOrDefaultAsync(x => x.EducationId == educationId);
        }
    }
}
