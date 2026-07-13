using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories.EducationRepositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecializationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Specialization>> GetAllAsync()
        {
            return await _dbContext.Specializations
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Specialization?> GetByIdAsync(int specializationId)
        {
            return await _dbContext.Specializations
                .FirstOrDefaultAsync(x => x.SpecializationId == specializationId);
        }
    }
}
