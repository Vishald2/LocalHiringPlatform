using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories.MasterData
{
    public class MasterRepository<T> : IMasterRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<T> _dbSet;

        public MasterRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
