using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces
{
    public interface IUniversityRepository
    {
        Task<List<University>> GetAllAsync();

        Task<University?> GetByIdAsync(int universityId);

        Task<List<University>> SearchAsync(string searchText);
    }
}
