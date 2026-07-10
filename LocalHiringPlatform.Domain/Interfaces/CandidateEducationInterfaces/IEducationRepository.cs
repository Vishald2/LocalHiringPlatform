using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface IEducationRepository
    {
        Task<List<Education>> GetAllAsync();

        Task<Education?> GetByIdAsync(int educationId);
    }
}

