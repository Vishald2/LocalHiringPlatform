using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ICandidateService
    {
        Task<List<RecommendedJobModel>> GetRecommendedJobsAsync(
                        Guid userId);
    }
}
