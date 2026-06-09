using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories
{
    public class EmployerProfileRepository : IEmployerProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployerProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(EmployerProfile profile)
        {
            _dbContext.EmployerProfiles.Add(profile);
        }

        public async Task<EmployerProfile?> GetByUserIdAsync(
            Guid userId)
        {
            return await _dbContext.EmployerProfiles
                .FirstOrDefaultAsync(
                    x => x.UserId == userId);
        }
    }
}
