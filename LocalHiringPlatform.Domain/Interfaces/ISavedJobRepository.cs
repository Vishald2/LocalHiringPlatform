using LocalHiringPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ISavedJobRepository
    {
        Task AddAsync(
            SavedJob savedJob);

        Task<SavedJob?>
            GetAsync(
                Guid userId,
                Guid jobId);

        Task<List<SavedJob>>
            GetByUserIdAsync(
                Guid userId);

        void Remove(
            SavedJob savedJob);
    }
}
