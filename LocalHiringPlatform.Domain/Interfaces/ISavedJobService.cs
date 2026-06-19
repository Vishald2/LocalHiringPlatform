using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ISavedJobService
    {
        Task SaveJobAsync(
            Guid userId,
            Guid jobId);

        Task RemoveSavedJobAsync(
            Guid userId,
            Guid jobId);

        Task<List<JobModel>>
            GetMySavedJobsAsync(
                Guid userId);
    }
}
