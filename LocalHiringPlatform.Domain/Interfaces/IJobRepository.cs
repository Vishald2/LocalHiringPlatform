using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJobRepository
{
    Task AddAsync(Job job);

    Task<List<Job>> GetAllAsync();

    Task<Job?> GetByIdAsync(Guid id);
}