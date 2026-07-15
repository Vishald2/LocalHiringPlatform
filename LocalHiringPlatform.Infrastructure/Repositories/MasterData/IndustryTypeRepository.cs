using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories.MasterData
{
    public class IndustryTypeRepository
        : MasterRepository<IndustryType>,
          IIndustryTypeRepository
    {
        public IndustryTypeRepository(
            ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IndustryType?> GetByIdAsync(
            int industryTypeId)
        {
            return await _dbSet.FirstOrDefaultAsync(
                x => x.IndustryTypeId == industryTypeId);
        }

        public async Task<List<IndustryType>> GetActiveAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }
    }
}
