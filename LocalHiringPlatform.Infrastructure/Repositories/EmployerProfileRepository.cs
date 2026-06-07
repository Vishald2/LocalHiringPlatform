using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Data;

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
    }
}
